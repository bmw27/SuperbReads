var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SuperbReads_Api>("superbreads-api");

builder.Build().Run();
