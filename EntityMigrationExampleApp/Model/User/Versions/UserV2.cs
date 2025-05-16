namespace EntityMigrationExampleApp.Model.User.Versions;

/// <summary>
/// Added first/last name split, removed the Name
/// </summary>
public class UserV2 : BaseUser
{
    /// <summary>
    /// First name of the user
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Last name of the user
    /// </summary>
    public required string LastName { get; set; }
}
