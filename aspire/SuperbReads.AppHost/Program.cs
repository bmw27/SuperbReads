using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgresUsername = builder.AddParameter("postgresUsername", secret: true);
var postgresPassword = builder.AddParameter("postgresPassword", secret: true);

var postgres = builder
    .AddPostgres("postgres", postgresUsername, postgresPassword)
    .WithEndpoint(name: "postgresendpoint", scheme: "tcp", targetPort: 5432, isProxied: false)
    .WithDataVolume(isReadOnly: false);

var postgresdb = postgres.AddDatabase(name: "postgresdb", databaseName: "superb_reads");

var dbUp = builder.AddProject<SuperbReads_Database>("superbreads-dbup")
    .WithArgs(context =>
    {
        context.Args.Add(new ConnectionStringReference(postgresdb.Resource, false));
    })
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddProject<SuperbReads_Api>("superbreads-api")
    .WithReference(postgresdb)
    .WaitFor(dbUp);

await builder.Build().RunAsync();
