namespace EntityMigrationFrameworkExampleApp.Model.User;

/// <summary>
/// Represents the base class for all versions of the User model
/// </summary>
public class BaseUser
{
    /// <summary>
    /// a shared property between all versions that will never change or get modified
    /// </summary>
    public Guid Id { get; set; }
}
