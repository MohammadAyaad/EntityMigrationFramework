using EntityMigration.Core;

namespace EntityMigration.Core;

public class MigrationManager<TBase> : IMigrationManager<TBase>
{
    private readonly IMigrationRegistry<TBase> _migrationRegistry;

    public MigrationManager(IMigrationRegistry<TBase> migrationRegistry)
    {
        _migrationRegistry = migrationRegistry;
    }

    public TTo Migrate<TFrom, TTo>(TFrom obj)
        where TFrom : TBase
        where TTo : TBase
    {
        var migrationDelegates = _migrationRegistry.GetMigrationPath<TFrom, TTo>();
        object current = obj!;

        foreach (var migrateDelegate in migrationDelegates)
        {
            current = migrateDelegate(current);
        }

        return (TTo)current;
    }
}
