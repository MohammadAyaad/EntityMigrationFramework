using EntityMigrationFramework.Core;
using EntityMigrationFramework.Internals;

namespace EntityMigrationFramework.Builder;

/// <summary>
/// Fluent interface for selecting and configuring migration registries
/// </summary>
/// <typeparam name="TBase">Base type for all versioned models</typeparam>
/// <typeparam name="NextStage">Next builder stage interface</typeparam>
public interface IMigrationBuilderRegistrySelector<TBase, NextStage>
{
    /// <summary>
    /// Configures a registry implementation for migration path resolution
    /// </summary>
    /// <typeparam name="TRegistry">Type of registry to use</typeparam>
    /// <param name="factory">Factory function that creates the registry</param>
    /// <returns>Next builder stage</returns>
    public NextStage UseRegistry<TRegistry>(Func<List<IMigration>, TRegistry> factory)
        where TRegistry : IMigrationRegistry<TBase>;
}
