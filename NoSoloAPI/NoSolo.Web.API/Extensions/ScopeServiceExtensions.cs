using Microsoft.AspNetCore.Authorization;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Repositories.Repositories;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Infrastructure.Repositories.Base;
using NoSolo.Infrastructure.Repositories.Organization;
using NoSolo.Infrastructure.Repositories.UOW;
using NoSolo.Infrastructure.Repositories.Users;
using NoSolo.Infrastructure.Services.Auth;
using NoSolo.Infrastructure.Services.Photos;
using NoSolo.Infrastructure.Services.Users;
using NoSolo.Infrastructure.Services.Utility;
using NoSolo.Web.API.Authorization;

namespace NoSolo.Web.API.Extensions;

public  static class ScopeServiceExtensions
{
    public static IServiceCollection AddScopedService(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IRecommendService, RecommendService>();
        services.AddScoped<IUserCredentialsService, UserCredentialsService>();
        
        return services;
    }

    public static IServiceCollection AddScopedRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }

    public static IServiceCollection AddScopedHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, HasProfileHandler>();

        return services;
    }
}