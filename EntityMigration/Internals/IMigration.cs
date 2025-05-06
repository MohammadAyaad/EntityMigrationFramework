namespace EntityMigration.Internals;

/// <summary>
/// Represents a migration between two types
/// !!!THIS SHOULD NOT BE IMPLEMENTED AS IT IS USED FOR INTERNALS AND RESTRICTION ONLY PURPOSES!!!
/// Use IMigration<in Src, out Dst> instead.
/// Migrations are single directional.
/// </summary>
public interface IMigration
{
    Type SourceType { get; }
    Type DestinationType { get; }
    object MigrateObject(object source);
}
