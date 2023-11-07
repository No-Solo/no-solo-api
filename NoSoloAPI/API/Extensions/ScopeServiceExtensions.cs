using API.Authorization;
using Core.Interfaces;
using Core.Interfaces.Data;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Extensions;

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