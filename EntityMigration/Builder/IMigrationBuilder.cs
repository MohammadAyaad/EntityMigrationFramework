using EntityMigration.Core;
using EntityMigration.Internals;

namespace EntityMigration.Builder;

/// <summary>
/// Finalization interface for the migration builder
/// </summary>
/// <typeparam name="TBase">The common base type for all versioned models</typeparam>
public interface IMigrationBuilder<TBase>
{
    /// <summary>
    /// Creates the migration manager with current configuration
    /// </summary>
    public IMigrationManager<TBase> Build();

    /// <summary>
    /// Advanced method for creating custom migration manager implementations
    /// </summary>
    /// <typeparam name="TMigrationManager">Custom manager type</typeparam>
    /// <param name="builder">Factory function for manager creation</param>
    public IMigrationManager<TBase> Build<TMigrationManager>(
        Func<List<IMigration>, IMigrationRegistry<TBase>, TMigrationManager> builder
    )
        where TMigrationManager : IMigrationManager<TBase>;
}
