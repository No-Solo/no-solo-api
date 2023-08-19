using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddAplicationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        });

        services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<DataBaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnectionString"));
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}