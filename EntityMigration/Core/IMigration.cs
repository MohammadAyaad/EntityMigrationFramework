using EntityMigration.Internals;

namespace EntityMigration.Core;

/// <summary>
/// Represents a strongly-typed migration from <typeparamref name="TSrc"/> to <typeparamref name="TDst"/>
/// </summary>
public interface IMigration<in TSrc, out TDst> : IMigration
{
    public TDst Migrate(TSrc src);

    Type IMigration.SourceType => typeof(TSrc);
    Type IMigration.DestinationType => typeof(TDst);
    object IMigration.MigrateObject(object source) => Migrate((TSrc)source)!;
}
