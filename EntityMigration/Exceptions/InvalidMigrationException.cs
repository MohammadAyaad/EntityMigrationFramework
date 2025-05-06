namespace EntityMigration.Exceptions;

public class InvalidMigrationException : Exception
{
    public InvalidMigrationException(string message)
        : base(message) { }
}
