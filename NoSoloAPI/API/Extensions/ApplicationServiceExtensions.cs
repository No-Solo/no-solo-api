using API.Errors;
using API.Helpers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    private const string Secret = "this is my custom Secret key for authentication";

    public static IServiceCollection AddAplicationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        
        services.AddSwaggerGen();
        services.AddSwaggerDocumentation();

        services.AddDbContext<DataBaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnectionString")
                , x => x.MigrationsAssembly("Infrastructure"));
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddApiBehaviourServices();

        services.AddScopeService();
        services.AddIdentityServices(configuration);
        
        return services;
    }
}