using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Errors;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    private const string Secret = "this is my custom Secret key for authentication";

    public static IServiceCollection AddAplicationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        });

        services.AddEndpointsApiExplorer();

        services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<DataBaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnectionString")
                , x => x.MigrationsAssembly("Infrastructure"));
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(error => error.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
                ValidIssuer = "saar",
                ValidateIssuer = true,
                ValidAudience = "saar-audience",
                ValidateAudience = true,
                NameClaimType = JwtRegisteredClaimNames.Sub,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = System.TimeSpan.Zero,
            };
        });
        
        return services;
    }
}