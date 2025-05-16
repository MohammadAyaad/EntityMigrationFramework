using EntityMigration.Internals;

namespace EntityMigration.Core;

/// <summary>
/// Represents a one-way strongly-typed migration from <typeparamref name="TSrc"/> to <typeparamref name="TDst"/>
/// </summary>
/// <typeparam name="TSrc">Source model type</typeparam>
/// <typeparam name="TDst">Destination model type</typeparam>
public interface IMigration<in TSrc, out TDst> : IMigration
{
    /// <summary>
    /// Executes the migration logic between specific model versions
    /// </summary>
    /// <param name="src">Source model instance</param>
    /// <returns>Migrated destination model instance</returns>
    public TDst Migrate(TSrc src);

    /// <summary>
    /// Gets the source type for this migration
    /// </summary>
    Type IMigration.DestinationType => typeof(TDst);

    /// <summary>
    /// Gets the destination type for this migration
    /// </summary>
    Type IMigration.SourceType => typeof(TSrc);

    /// <summary>
    /// Executes migration using type-erased input
    /// </summary>
    /// <param name="source">Source object to migrate</param>
    /// <returns>Migrated object</returns>
    /// <exception cref="InvalidCastException">Thrown if source type doesn't match TSrc</exception>
    object IMigration.MigrateObject(object source) => Migrate((TSrc)source)!;
}
