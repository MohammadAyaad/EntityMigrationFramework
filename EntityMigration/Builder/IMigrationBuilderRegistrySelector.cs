using EntityMigration.Core;
using EntityMigration.Internals;

namespace EntityMigration.Builder;

public interface IMigrationBuilderRegistrySelector<TBase, NextStage>
{
    public NextStage UseRegistry<TRegistry>(Func<List<IMigration>, TRegistry> factory)
        where TRegistry : IMigrationRegistry<TBase>;
}
