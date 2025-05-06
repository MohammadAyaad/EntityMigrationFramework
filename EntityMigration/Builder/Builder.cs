using EntityMigration.Core;
using EntityMigration.Internals;

namespace EntityMigration.Builder;

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

    public MigrationBuilder()
    {
        _migrations = new List<IMigration>();
        _registry = null;
    }

    public IMigrationBuilderMigrationCollector<
        TBase,
        IMigrationBuilderRegistrySelector<TBase, IMigrationBuilder<TBase>>
    > AddMigration<TMigration, TSrc, TDst>()
        where TMigration : IMigration<TSrc, TDst>, new()
    {
        this._migrations.Add(new TMigration());
        return this;
    }

    public IMigrationBuilderMigrationCollector<
        TBase,
        IMigrationBuilderRegistrySelector<TBase, IMigrationBuilder<TBase>>
    > AddMigration<TMigration, TSrc, TDst>(TMigration migration)
        where TMigration : IMigration<TSrc, TDst>
    {
        this._migrations.Add(migration);
        return this;
    }

    public IMigrationManager<TBase> Build()
    {
        return new MigrationManager<TBase>(this._registry!);
    }

    public IMigrationManager<TBase> Build<TMigrationManager>(
        Func<List<IMigration>, IMigrationRegistry<TBase>, TMigrationManager> builder
    )
        where TMigrationManager : IMigrationManager<TBase>
    {
        throw new NotImplementedException();
    }

    public IMigrationBuilderRegistrySelector<TBase, IMigrationBuilder<TBase>> Prepare()
    {
        return this;
    }

    public IMigrationBuilder<TBase> UseRegistry<TRegistry>(
        Func<List<IMigration>, TRegistry> factory
    )
        where TRegistry : IMigrationRegistry<TBase>
    {
        this._registry = factory(this._migrations);
        return this;
    }
}
