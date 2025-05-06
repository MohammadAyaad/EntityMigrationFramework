using EntityMigration.Core;

namespace EntityMigration.Builder;

public interface IMigrationBuilderMigrationCollector<TBase, NextStage>
{
    public IMigrationBuilderMigrationCollector<TBase, NextStage> AddMigration<
        TMigration,
        TSrc,
        TDst
    >()
        where TMigration : IMigration<TSrc, TDst>, new();
    public IMigrationBuilderMigrationCollector<TBase, NextStage> AddMigration<
        TMigration,
        TSrc,
        TDst
    >(TMigration migration)
        where TMigration : IMigration<TSrc, TDst>;

    public NextStage Prepare();
}
