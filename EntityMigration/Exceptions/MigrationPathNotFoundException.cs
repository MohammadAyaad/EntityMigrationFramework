namespace EntityMigration.Exceptions;

public class MigrationPathNotFoundException : Exception
{
    public MigrationPathNotFoundException(string message)
        : base(message) { }
}
