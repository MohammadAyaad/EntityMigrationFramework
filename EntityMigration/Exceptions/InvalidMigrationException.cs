namespace EntityMigration.Exceptions;

/// <summary>
/// Exception thrown when invalid migration configuration is detected
/// </summary>
public class InvalidMigrationException : MigrationException
{
    /// <summary>
    /// Initializes a new instance with a message
    /// </summary>
    public InvalidMigrationException(string message)
        : base(message) { }
}
