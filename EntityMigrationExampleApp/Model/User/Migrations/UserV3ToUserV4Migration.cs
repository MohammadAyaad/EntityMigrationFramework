using EntityMigration.Core;
using EntityMigrationExampleApp.Model.User.Versions;

namespace EntityMigrationExampleApp.Model.User.Migrations;

/// <summary>
/// Adds Job information field
/// </summary>
public class UserV3ToUserV4Migration : IMigration<UserV3, UserV4>
{
    /// <summary>
    /// Converts from UserV3 to UserV4, sets the default job to an empty string
    /// </summary>
    /// <param name="src">Source UserV2 instance</param>
    /// <returns>New UserV3 instance</returns>
    public UserV4 Migrate(UserV3 src)
    {
        return new UserV4()
        {
            Id = src.Id,
            FirstName = src.FirstName,
            LastName = src.LastName,
            Job = "", // Default empty value
            Age = src.Age,
        };
    }
}
