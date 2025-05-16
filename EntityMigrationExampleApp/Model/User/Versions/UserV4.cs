namespace EntityMigrationExampleApp.Model.User.Versions;

/// <summary>
/// Added job information field
/// </summary>
public class UserV4 : BaseUser
{
    /// <inheritdoc/>
    public required string FirstName { get; set; }

    /// <inheritdoc/>
    public required string LastName { get; set; }

    /// <summary>
    /// The job of the user
    /// </summary>
    public required string Job { get; set; }

    /// <inheritdoc/>
    public int Age { get; set; }
}
