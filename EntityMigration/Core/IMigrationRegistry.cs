namespace EntityMigration.Core;

public interface IMigrationRegistry<TBase>
{
    List<Func<object, object>> GetMigrationPath<TFrom, TTo>()
        where TFrom : TBase
        where TTo : TBase;
}
