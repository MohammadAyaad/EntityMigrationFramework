namespace EntityMigrationFramework.Exceptions;

/// <summary>
/// Exception thrown when no valid migration path exists between requested types
/// </summary>
public class MigrationPathNotFoundException : MigrationException
{
    /// <summary>
    /// Initializes a new instance with a message
    /// </summary>
    public MigrationPathNotFoundException(string message)
        : base(message) { }
}
