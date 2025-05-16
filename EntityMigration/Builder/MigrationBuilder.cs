using EntityMigration.Core;
using EntityMigration.Internals;

namespace EntityMigration.Builder;

/// <summary>
/// Fluent builder for configuring and creating migration managers
/// </summary>
/// <typeparam name="TBase">The common base type for all versioned models</typeparam>
public class MigrationBuilder<TBase>
    : IMigrationBuilderMigrationCollector<
        TBase,
        IMigrationBuilderRegistrySelector<TBase, IMigrationBuilder<TBase>>
    >,
        IMigrationBuilderRegistrySelector<TBase, IMigrationBuilder<TBase>>,
        IMigrationBuilder<TBase>
{
    private List<IMigration> _migrations;
    private IMigrationRegistry<TBase>? _registry;

    /// <summary>
    /// Initializes a new instance of the migration builder
    /// </summary>
    public MigrationBuilder()
    {
        _migrations = new List<IMigration>();
        _registry = null;
    }

    /// <summary>
    /// Adds a migration implementation with parameterless constructor
    /// </summary>
    /// <typeparam name="TMigration">Migration implementation type</typeparam>
    /// <typeparam name="TSrc">Source type for the migration</typeparam>
    /// <typeparam name="TDst">Destination type for the migration</typeparam>
    /// <returns>The builder instance for method chaining</returns>
    public IMigrationBuilderMigrationCollector<
        TBase,
        IMigrationBuilderRegistrySelector<TBase, IMigrationBuilder<TBase>>
    > AddMigration<TMigration, TSrc, TDst>()
        where TMigration : IMigration<TSrc, TDst>, new()
    {
        this._migrations.Add(new TMigration());
        return this;
    }

    /// <summary>
    /// Adds a pre-configured migration instance
    /// </summary>
    /// <typeparam name="TMigration">Migration implementation type</typeparam>
    /// <typeparam name="TSrc">Source type for the migration</typeparam>
    /// <typeparam name="TDst">Destination type for the migration</typeparam>
    /// <param name="migration">Configured migration instance</param>
    /// <returns>The builder instance for method chaining</returns>
    public IMigrationBuilderMigrationCollector<
        TBase,
        IMigrationBuilderRegistrySelector<TBase, IMigrationBuilder<TBase>>
    > AddMigration<TMigration, TSrc, TDst>(TMigration migration)
        where TMigration : IMigration<TSrc, TDst>
    {
        this._migrations.Add(migration);
        return this;
    }

    /// <summary>
    /// Builds the migration manager using the configured registry
    /// </summary>
    /// <returns>Initialized migration manager instance</returns>
    /// <exception cref="InvalidOperationException">Thrown if registry is not configured</exception>
    public IMigrationManager<TBase> Build()
    {
        return new MigrationManager<TBase>(this._registry!);
    }

    /// <summary>
    /// Advanced builder method for custom migration manager implementations
    /// </summary>
    /// <typeparam name="TMigrationManager">Custom migration manager type</typeparam>
    /// <param name="builder">Factory function for creating the manager</param>
    /// <returns>Custom migration manager instance</returns>
    /// <remarks>
    /// This method allows using custom implementations of IMigrationManager
    /// while leveraging the builder configuration
    /// </remarks>
    public IMigrationManager<TBase> Build<TMigrationManager>(
        Func<List<IMigration>, IMigrationRegistry<TBase>, TMigrationManager> builder
    )
        where TMigrationManager : IMigrationManager<TBase>
    {
        return builder(this._migrations, this._registry!);
    }

    /// <summary>
    /// Finalizes migration registration and transitions to registry configuration
    /// </summary>
    /// <returns>Registry configuration interface</returns>

    public IMigrationBuilderRegistrySelector<TBase, IMigrationBuilder<TBase>> Prepare()
    {
        return this;
    }

    /// <summary>
    /// Configures the migration registry implementation
    /// </summary>
    /// <typeparam name="TRegistry">Registry implementation type</typeparam>
    /// <param name="factory">Factory function that creates the registry</param>
    /// <returns>The builder instance for finalization</returns>
    public IMigrationBuilder<TBase> UseRegistry<TRegistry>(
        Func<List<IMigration>, TRegistry> factory
    )
        where TRegistry : IMigrationRegistry<TBase>
    {
        this._registry = factory(this._migrations);
        return this;
    }
}
