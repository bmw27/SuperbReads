using Scalar.AspNetCore;
using SuperbReads.ServiceDefaults;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => options.Servers = []);
}

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
