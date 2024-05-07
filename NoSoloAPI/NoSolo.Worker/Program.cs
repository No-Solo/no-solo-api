using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoSolo.Worker.Extensions;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddWorkerService(builder.Configuration);

var app = builder.Build();

app.Run();