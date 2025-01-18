using System.Reflection;

using DbUp;
using DbUp.Engine;

string connectionString =
    args.FirstOrDefault()
    ?? "Server=(local)\\SqlExpress; Database=MyApp; Trusted_connection=true";

EnsureDatabase.For.PostgresqlDatabase(connectionString);

UpgradeEngine upgrader =
    DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .Build();

DatabaseUpgradeResult result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;

#pragma warning disable CA1303
Console.WriteLine("Success!");
#pragma warning restore CA1303

Console.ResetColor();
return 0;