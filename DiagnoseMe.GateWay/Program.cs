using System.Net;
using DiagnoseMe.GateWay;
using Ocelot.Middleware;

ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
{
    builder.Services.AddPresentation(builder.Configuration);
}
var app = builder.Build();
{
    await app.UseOcelot();
}

app.Run();
