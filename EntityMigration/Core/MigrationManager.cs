using EntityMigration.Core;

namespace EntityMigration.Core;

/// <summary>
/// Default implementation of migration management
/// </summary>
/// <typeparam name="TBase">Common base type for all model versions</typeparam>
/// <exception cref="EntityMigration.Exceptions.MigrationPathNotFoundException">
/// Thrown when no valid migration path exists between requested types
/// </exception>
public class MigrationManager<TBase> : IMigrationManager<TBase>
{
    private readonly IMigrationRegistry<TBase> _migrationRegistry;

    /// <summary>
    /// Initializes a new migration manager
    /// </summary>
    /// <param name="migrationRegistry">Configured migration registry</param>
    public MigrationManager(IMigrationRegistry<TBase> migrationRegistry)
    {
        _migrationRegistry = migrationRegistry;
    }

    /// <inheritdoc/>
    public TTo Migrate<TFrom, TTo>(TFrom obj)
        where TFrom : TBase
        where TTo : TBase
    {
        var migrationDelegates = _migrationRegistry.GetMigrationPath<TFrom, TTo>();
        object current = obj!;

        foreach (var migrateDelegate in migrationDelegates)
        {
            current = migrateDelegate(current);
        }

        return (TTo)current;
    }
}
