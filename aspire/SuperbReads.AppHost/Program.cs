using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<SuperbReads_Api>("superbreads-api");

await builder.Build().RunAsync();