using System.Reflection;
using DbUp;

var connectionString = args.FirstOrDefault();

if (string.IsNullOrWhiteSpace(connectionString))
{
#pragma warning disable CA1303
    Console.WriteLine("Connection string is required.");
#pragma warning restore CA1303
    return -1;
}

EnsureDatabase.For.PostgresqlDatabase(connectionString);

var upgrader =
    DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .Build();

var result = upgrader.PerformUpgrade();

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
