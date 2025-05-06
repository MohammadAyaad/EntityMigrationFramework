namespace EntityMigration.Core;

public interface IMigrationManager<TBase>
{
    TTo Migrate<TFrom, TTo>(TFrom obj)
        where TFrom : TBase
        where TTo : TBase;
}
