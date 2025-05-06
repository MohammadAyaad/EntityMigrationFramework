using EntityMigration.Core;
using EntityMigration.Internals;

namespace EntityMigration.Builder;

public interface IMigrationBuilder<TBase>
{
    public IMigrationManager<TBase> Build();
    public IMigrationManager<TBase> Build<TMigrationManager>(
        Func<List<IMigration>, IMigrationRegistry<TBase>, TMigrationManager> builder
    )
        where TMigrationManager : IMigrationManager<TBase>;
}
