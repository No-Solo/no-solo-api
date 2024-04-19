using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Repositories.Auth;
using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Repositories.FeedBack;
using NoSolo.Abstractions.Repositories.Requests;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Contacts;
using NoSolo.Abstractions.Services.Email;
using NoSolo.Abstractions.Services.Memberships;
using NoSolo.Abstractions.Services.Offers;
using NoSolo.Abstractions.Services.Organizations;
using NoSolo.Abstractions.Services.Photos;
using NoSolo.Abstractions.Services.Requests;
using NoSolo.Abstractions.Services.Tags;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Infrastructure.Repositories.Auth;
using NoSolo.Infrastructure.Repositories.Base;
using NoSolo.Infrastructure.Repositories.FeedBack;
using NoSolo.Infrastructure.Repositories.Requests;
using NoSolo.Infrastructure.Repositories.UOW;
using NoSolo.Infrastructure.Services.Auth;
using NoSolo.Infrastructure.Services.Contacts;
using NoSolo.Infrastructure.Services.Email;
using NoSolo.Infrastructure.Services.Offers;
using NoSolo.Infrastructure.Services.Organizations;
using NoSolo.Infrastructure.Services.Photos;
using NoSolo.Infrastructure.Services.Requests;
using NoSolo.Infrastructure.Services.Tags;
using NoSolo.Infrastructure.Services.Users;
using NoSolo.Infrastructure.Services.Utility;

namespace NoSolo.Web.API.Extensions;

public static class ScopeServiceExtensions
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
        services.AddScoped<IFeedBackService, FeedBackService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IPasswordResetService, PasswordResetService>();
        services.AddScoped<IVerificationCodeService, VerificationCodeService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IOfferService, OfferService>();
        services.AddScoped<IPhotoService, PhotoService>();

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
        services.AddScoped<IUserRequestService, UserRequestService>();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddScopedOrganizationServices(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationPhotoService, OrganizationPhotoService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IOrganizationContactService, OrganizationContactService>();
        services.AddScoped<IOrganizationOfferService, OrganizationOfferService>();
        services.AddScoped<IOrganizationRequestService, OrganizationRequestService>();

        return services;
    }

    public static IServiceCollection AddScopedRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IFeedBackRepository, FeedBackRepositories>();

        services.AddScoped<IOrganizationRequestRepository, OrganizationRequestRepository>();
        services.AddScoped<IUserRequestRepository, UserRequestRepository>();

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