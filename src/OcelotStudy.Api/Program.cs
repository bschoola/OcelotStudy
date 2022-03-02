using Delta.Standard.Gateway.Api.Configurations;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot(builder.Configuration);

builder.Services.ConfigureHealthCheck(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapEndpointsHealthCheck();

await app.UseOcelot();

app.Run();
