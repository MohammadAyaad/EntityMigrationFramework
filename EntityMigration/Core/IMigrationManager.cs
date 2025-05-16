namespace EntityMigration.Core;

/// <summary>
/// Manages migration operations between different model versions
/// </summary>
/// <typeparam name="TBase">Common base type for all model versions</typeparam>
public interface IMigrationManager<TBase>
{
    /// <summary>
    /// Migrates an object between two model versions
    /// </summary>
    /// <typeparam name="TFrom">Source model type</typeparam>
    /// <typeparam name="TTo">Destination model type</typeparam>
    /// <param name="obj">Instance to migrate</param>
    /// <returns>Migrated instance of destination type</returns>
    /// <exception cref="EntityMigration.Exceptions.MigrationPathNotFoundException">No valid migration path exists</exception>
    /// <exception cref="EntityMigration.Exceptions.InvalidMigrationException">Error during migration execution</exception>
    TTo Migrate<TFrom, TTo>(TFrom obj)
        where TFrom : TBase
        where TTo : TBase;
}
