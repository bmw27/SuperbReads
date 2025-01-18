using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume(isReadOnly: false);
var postgresdb = postgres.AddDatabase("postgresdb");

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
