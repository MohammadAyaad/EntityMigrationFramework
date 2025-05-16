using EntityMigration.Core;
using EntityMigrationExampleApp.Model.User.Versions;

namespace EntityMigrationExampleApp.Model.User.Migrations;

/// <summary>
/// Handles conversion from UserV1 to UserV2 by splitting name
/// </summary>
public class UserV1ToUserV2Migration : IMigration<UserV1, UserV2>
{
    /// <summary>
    /// Converts from UserV1 to UserV2, Splits the Name property into FirstName and LastName
    /// </summary>
    /// <param name="src">Source UserV1 instance</param>
    /// <returns>New UserV2 instance</returns>
    public UserV2 Migrate(UserV1 src)
    {
        return new UserV2()
        {
            Id = src.Id,
            FirstName = src.Name,
            LastName = "",
        };
    }
}
