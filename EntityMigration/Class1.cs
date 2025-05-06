using System.Reflection;
using EntityMigration.Builder;
using EntityMigration.Core;
using EntityMigration.Core.MigrationRegistry;
using EntityMigration.Internals;

namespace EntityMigration;

public static class Program
{
    public static void Main(string[] args)
    {
        IMigrationManager<BaseUser> manager = new MigrationBuilder<BaseUser>()
            .AddMigration<UserV1ToUserV2Migration, UserV1, UserV2>()
            .AddMigration<UserV2ToUserV3Migration, UserV2, UserV3>()
            .AddMigration<UserV3ToUserV4Migration, UserV3, UserV4>()
            .Prepare()
            .UseDefaultGraphRegistry()
            .Build();

        UserV1 a = new UserV1() { Name = "XYZA" };

        Console.WriteLine($"V1:\n{Newtonsoft.Json.JsonConvert.SerializeObject(a)}");
        UserV2 a2 = manager.Migrate<UserV1, UserV2>(a);

        Console.WriteLine($"V2:\n{Newtonsoft.Json.JsonConvert.SerializeObject(a2)}");
        UserV4 a3 = manager.Migrate<UserV1, UserV4>(a);
        Console.WriteLine($"V3:\n{Newtonsoft.Json.JsonConvert.SerializeObject(a3)}");
    }
}

public class UserV1ToUserV2Migration : IMigration<UserV1, UserV2>
{
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

public class UserV2ToUserV3Migration : IMigration<UserV2, UserV3>
{
    public UserV3 Migrate(UserV2 src)
    {
        return new UserV3()
        {
            Id = src.Id,
            FirstName = src.FirstName,
            LastName = src.LastName,
            Age = -1,
        };
    }
}

public class UserV3ToUserV4Migration : IMigration<UserV3, UserV4>
{
    public UserV4 Migrate(UserV3 src)
    {
        return new UserV4()
        {
            Id = src.Id,
            FirstName = src.FirstName,
            LastName = src.LastName,
            Job = "",
            Age = src.Age,
        };
    }
}

public class BaseUser
{
    public Guid Id { get; set; }
}

public class UserV1 : BaseUser
{
    public required string Name { get; set; }
}

public class UserV2 : BaseUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public class UserV3 : BaseUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int Age { get; set; }
}

public class UserV4 : BaseUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Job { get; set; }
    public int Age { get; set; }
}
