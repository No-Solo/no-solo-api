using NoSolo.Web.API.Extensions;
using NoSolo.Web.API.Middleware;
using NoSolo.Infrastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NoSolo.Infrastructure.Data.DbContext;
using NoSolo.Web.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services
    .AddControllers(options => { options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; })
    .AddNewtonsoftJson(options => { options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; });

builder.Services.AddAplicationService(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ConsoleHelper.ShowInfo("DefaultConnectionString", builder);
    ConsoleHelper.ShowInfo("FeedBackConnectionString", builder);
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwaggerDocumention();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<DataBaseContext>();
try
{
    await context.Database.MigrateAsync();
}
catch (Exception exception)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(exception, "An error occured during migration");
}

await Seed.SeedRoles(services);

app.Run();