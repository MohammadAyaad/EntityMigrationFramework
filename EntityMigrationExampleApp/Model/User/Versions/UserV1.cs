namespace EntityMigrationExampleApp.Model.User.Versions;

/// <summary>
/// Initial user model with basic name property
/// </summary>
public class UserV1 : BaseUser
{
    /// <summary>
    /// Full name of the user
    /// </summary>
    public required string Name { get; set; }
}
