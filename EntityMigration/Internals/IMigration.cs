namespace EntityMigration.Internals;

public interface IMigration
{
    public Type SourceType { get; }
    public Type DestinationType { get; }
    object MigrateObject(object source);
}
