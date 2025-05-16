using EntityMigration.Core;

namespace EntityMigration.Builder;

/// <summary>
/// Interface for collecting migration implementations during configuration
/// </summary>
/// <typeparam name="TBase">Common base type for models</typeparam>
/// <typeparam name="NextStage">Next configuration stage interface</typeparam>
public interface IMigrationBuilderMigrationCollector<TBase, NextStage>
{
    /// <summary>
    /// Registers a migration type with parameterless constructor
    /// </summary>
    /// <typeparam name="TMigration">Migration implementation type</typeparam>
    /// <typeparam name="TSrc">Source model type</typeparam>
    /// <typeparam name="TDst">Destination model type</typeparam>
    public IMigrationBuilderMigrationCollector<TBase, NextStage> AddMigration<
        TMigration,
        TSrc,
        TDst
    >()
        where TMigration : IMigration<TSrc, TDst>, new();

    /// <summary>
    /// Registers a pre-initialized migration instance
    /// </summary>
    /// <typeparam name="TMigration">Migration implementation type</typeparam>
    /// <typeparam name="TSrc">Source model type</typeparam>
    /// <typeparam name="TDst">Destination model type</typeparam>
    /// <param name="migration">Initialized migration instance</param>
    public IMigrationBuilderMigrationCollector<TBase, NextStage> AddMigration<
        TMigration,
        TSrc,
        TDst
    >(TMigration migration)
        where TMigration : IMigration<TSrc, TDst>;

    /// <summary>
    /// Completes migration registration and advances to the next stage
    /// </summary>
    public NextStage Prepare();
}
