using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Tags;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Infrastructure.Repositories.Base;
using NoSolo.Infrastructure.Repositories.UOW;
using NoSolo.Infrastructure.Services.Auth;
using NoSolo.Infrastructure.Services.Contacts;
using NoSolo.Infrastructure.Services.Offers;
using NoSolo.Infrastructure.Services.Organizations;
using NoSolo.Infrastructure.Services.Photos;
using NoSolo.Infrastructure.Services.Users;

namespace NoSolo.Web.API.Extensions;

public  static class ScopeServiceExtensions
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScopedUserServices();
        services.AddScopedOrganizationServices();

        services.AddUtilityServices();
        
        return services;
    }

    public static IServiceCollection AddUtilityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IRecommendService, RecommendService>();

        return services;
    }
    
    public static IServiceCollection AddScopedUserServices(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IUserCredentialsService, UserCredentialsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserContactService, UserContactService>();
        services.AddScoped<IUserTagsService, UserTagsService>();
        services.AddScoped<IUserPhotoService, UserPhotoService>();
        services.AddScoped<IUserOfferService, UserOfferService>();
        
        return services;
    }

    public static IServiceCollection AddScopedOrganizationServices(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationPhotoService, OrganizationPhotoService>();
        services.AddScoped<IOrganizaitonService, OrganizationService>();
        services.AddScoped<IOrganizationContactService, OrganizationContactService>();
        services.AddScoped<IOrganizationOfferService, OrganizationOfferService>();

        return services;
    }
    
    public static IServiceCollection AddScopedRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }

    // public static IServiceCollection AddScopedHandlers(this IServiceCollection services)
    // {
    //     services.AddSingleton<IAuthorizationHandler, HasProfileHandler>();
    //
    //     return services;
    // }
}