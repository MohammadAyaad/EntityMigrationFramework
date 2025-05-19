namespace EntityMigrationFrameworkExampleApp.Model.User.Versions;

/// <summary>
/// Added age property with default value
/// </summary>
public class UserV3 : BaseUser
{
    /// <inheritdoc/>
    public required string FirstName { get; set; }

    /// <inheritdoc/>
    public required string LastName { get; set; }

    /// <summary>
    /// The age of the user
    /// </summary>
    public int Age { get; set; }
}
