using NoSolo.Infrastructure;
using NoSolo.Infrastructure.Repositories;
using NoSolo.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using NoSolo.Abstractions.Data;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Repositories;
using NoSolo.Abstractions.Repositories.Repositories;
using NoSolo.Abstractions.Services;
using NoSolo.Abstractions.Services.Services;
using NoSolo.Presentation.Authorization;

namespace NoSolo.Presentation.Extensions;

public  static class ScopeServiceExtensions
{
    public static IServiceCollection AddScopeService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();


        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IRecommendService, RecommendService>();

        services.AddScoped<IResponseCacheService, ResponseCacheService>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddSingleton<IAuthorizationHandler, HasProfileHandler>();

        return services;
    }
}