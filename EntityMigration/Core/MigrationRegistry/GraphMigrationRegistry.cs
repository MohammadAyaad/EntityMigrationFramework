using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntityMigration.Builder;
using EntityMigration.Core;
using EntityMigration.Exceptions;
using EntityMigration.Internals;

namespace EntityMigration.Core.MigrationRegistry;

public class GraphMigrationRegistry<TBase> : IMigrationRegistry<TBase>
{
    private readonly Dictionary<
        Type,
        List<(Type DstType, Func<object, object> Migrate)>
    > _adjacencyList = new();

    public GraphMigrationRegistry(IEnumerable<IMigration> migrations)
    {
        foreach (var migration in migrations)
        {
            var migrationInterface = migration
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMigration<,>)
                );

            if (migrationInterface == null)
                throw new InvalidMigrationException(
                    $"Migration {migration.GetType()} must implement IMigration<,>"
                );

            var srcType = migration.SourceType;
            var dstType = migration.DestinationType;

            if (
                !typeof(TBase).IsAssignableFrom(srcType) || !typeof(TBase).IsAssignableFrom(dstType)
            )
                throw new ArgumentException($"Migration types must inherit from {typeof(TBase)}");

            var migrationDelegate = CreateMigrationDelegate(migration);

            if (!_adjacencyList.TryGetValue(srcType, out var connections))
            {
                connections = new List<(Type, Func<object, object>)>();
                _adjacencyList[srcType] = connections;
            }
            connections.Add((dstType, migrationDelegate));
        }
    }

    private static Func<object, object> CreateMigrationDelegate(IMigration migration)
    {
        var interfaceType = migration.GetType().GetInterface(typeof(IMigration).FullName!);
        if (interfaceType == null)
            throw new InvalidMigrationException(
                $"Migration {migration.GetType()} does not implement IMigration interface"
            );

        var methodInfo = interfaceType.GetMethod(nameof(IMigration.MigrateObject));
        if (methodInfo == null)
            throw new InvalidMigrationException(
                $"Migration {migration.GetType()} is missing MigrateObject implementation"
            );

        var param = Expression.Parameter(typeof(object));
        var call = Expression.Call(Expression.Constant(migration), methodInfo, param);
        return Expression.Lambda<Func<object, object>>(call, param).Compile();
    }

    List<Func<object, object>> IMigrationRegistry<TBase>.GetMigrationPath<TFrom, TTo>()
    {
        var startType = typeof(TFrom);
        var endType = typeof(TTo);

        if (startType == endType)
            return new List<Func<object, object>>();

        var queue = new Queue<(Type CurrentType, List<Func<object, object>> Path)>();
        var visited = new Dictionary<Type, List<Func<object, object>>>();

        queue.Enqueue((startType, new List<Func<object, object>>()));
        visited[startType] = new List<Func<object, object>>();

        while (queue.Count > 0)
        {
            var (currentType, currentPath) = queue.Dequeue();

            if (!_adjacencyList.TryGetValue(currentType, out var connections))
                continue;

            foreach (var (dstType, migration) in connections)
            {
                if (
                    visited.TryGetValue(dstType, out var existingPath)
                    && existingPath.Count <= currentPath.Count + 1
                )
                    continue;

                var newPath = new List<Func<object, object>>(currentPath) { migration };

                if (dstType == endType)
                    return newPath;

                visited[dstType] = newPath;
                queue.Enqueue((dstType, newPath));
            }
        }

        throw new MigrationPathNotFoundException(
            $"No migration path found from {startType} to {endType}"
        );
    }
}

public static class GraphMigrationRegistryBuilderExtension
{
    public static NextStage UseDefaultGraphRegistry<TBase, NextStage>(
        this IMigrationBuilderRegistrySelector<TBase, NextStage> builder
    )
    {
        return builder.UseRegistry(m => new GraphMigrationRegistry<TBase>(m));
    }
}
