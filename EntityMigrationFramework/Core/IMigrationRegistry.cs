namespace EntityMigrationFramework.Core;

/// <summary>
/// Defines the contract for migration path resolution strategies
/// </summary>
/// <typeparam name="TBase">Base type for all versioned models</typeparam>
public interface IMigrationRegistry<TBase>
{
    /// <summary>
    /// Resolves a migration path between two model versions
    /// </summary>
    /// <typeparam name="TFrom">Source model type</typeparam>
    /// <typeparam name="TTo">Destination model type</typeparam>
    /// <returns>Ordered sequence of migration delegates</returns>
    /// <exception cref="EntityMigrationFramework.Exceptions.MigrationPathNotFoundException">
    /// Thrown when no valid path exists between versions
    /// </exception>
    List<Func<object, object>> GetMigrationPath<TFrom, TTo>()
        where TFrom : TBase
        where TTo : TBase;
}
