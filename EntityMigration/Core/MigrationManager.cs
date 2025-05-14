using EntityMigration.Core;

namespace EntityMigration.Core;

/// <summary>
/// Main entry point for migration operations
/// </summary>
/// <typeparam name="TBase">Base type for all versioned models</typeparam>
public class MigrationManager<TBase> : IMigrationManager<TBase>
{
    private readonly IMigrationRegistry<TBase> _migrationRegistry;

    public MigrationManager(IMigrationRegistry<TBase> migrationRegistry)
    {
        _migrationRegistry = migrationRegistry;
    }

    /// <summary>
    /// Migrates an object between two versions
    /// </summary>
    /// <param name="obj">Source object to migrate</param>
    /// <returns>New instance of destination type</returns>
    /// <exception cref="T:EntityMigration.MigrationPathNotFoundException">
    /// Thrown when no valid migration path exists
    /// </exception>
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
