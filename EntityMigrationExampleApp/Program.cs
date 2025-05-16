using System.Reflection;
using EntityMigration.Builder;
using EntityMigration.Core;
using EntityMigration.Core.MigrationRegistry;
using EntityMigrationExampleApp.Model.User;
using EntityMigrationExampleApp.Model.User.Migrations;
using EntityMigrationExampleApp.Model.User.Versions;
using Newtonsoft.Json;

namespace EntityMigrationExampleApp;

/// <summary>
/// The main class of the example app
/// </summary>
public static class Program
{
    /// <summary>
    /// The main entry point of the example app
    /// </summary>
    public static void Main(string[] args)
    {
        // Configure migration manager
        IMigrationManager<BaseUser> manager = new MigrationBuilder<BaseUser>()
            .AddMigration<UserV1ToUserV2Migration, UserV1, UserV2>()
            .AddMigration<UserV2ToUserV3Migration, UserV2, UserV3>()
            .AddMigration<UserV3ToUserV4Migration, UserV3, UserV4>()
            .Prepare()
            .UseDefaultGraphRegistry()
            .Build();

        // Execute migrations
        var v1User = new UserV1 { Name = "XYZA" };

        Console.WriteLine($"V1:\n{Serialize(v1User)}");

        // Direct migration to V2
        var v2User = manager.Migrate<UserV1, UserV2>(v1User);
        Console.WriteLine($"V2:\n{Serialize(v2User)}");

        // Chained migration to V4
        var v4User = manager.Migrate<UserV1, UserV4>(v1User);
        Console.WriteLine($"V4:\n{Serialize(v4User)}");
    }

    private static string Serialize(object obj) =>
        Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented);
}
