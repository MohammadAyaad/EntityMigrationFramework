using EntityMigrationFramework.Core;
using EntityMigrationFrameworkExampleApp.Model.User.Versions;

namespace EntityMigrationFrameworkExampleApp.Model.User.Migrations;

/// <summary>
/// Adds Age property with default value
/// </summary>
public class UserV2ToUserV3Migration : IMigration<UserV2, UserV3>
{
    /// <summary>
    /// Converts from UserV2 to UserV3, sets the default age to -1
    /// </summary>
    /// <param name="src">Source UserV2 instance</param>
    /// <returns>New UserV3 instance</returns>
    public UserV3 Migrate(UserV2 src)
    {
        return new UserV3()
        {
            Id = src.Id,
            FirstName = src.FirstName,
            LastName = src.LastName,
            Age = -1, // Default invalid value
        };
    }
}
