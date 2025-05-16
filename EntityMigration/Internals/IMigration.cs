namespace EntityMigration.Internals;

/// <summary>
/// [!WARNING DO NOT USE THIS!] Base interface for migrations
/// </summary>
public interface IMigration
{
    /// <summary>
    /// Source type for the migration
    /// </summary>
    public Type SourceType { get; }

    /// <summary>
    /// Destination type for the migration
    /// </summary>
    public Type DestinationType { get; }

    /// <summary>
    /// Executes migration on a boxed object
    /// </summary>
    /// <param name="source">Source object to migrate</param>
    object MigrateObject(object source);
}
