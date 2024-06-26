﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NoSolo.Abstractions.Services.Email;
using NoSolo.Infrastructure.Data.DbContext;
using NoSolo.Infrastructure.Services.Email;
using NoSolo.Infrastructure.Services.Email.Options;
using NoSolo.Infrastructure.Services.Photos.Settings;

namespace NoSolo.Web.API.Extensions;

[ExcludeFromCodeCoverage]
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        services.AddSwaggerGen();
        services.AddSwaggerDocumentation();

        services.AddDbContext<DataBaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnectionString")
                , x => x.MigrationsAssembly("NoSolo.Infrastructure.Data"));
        });

        services.AddDbContext<FeedBackContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("FeedBackConnectionString"),
                x => x.MigrationsAssembly("NoSolo.Infrastructure.Data"));
        });

        // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddApiBehaviourServices();

        services.AddScopedServices();
        // services.AddScopedHandlers();
        services.AddScopedRepositories();

        services.AddEmailService(configuration);

        services.AddIdentityServices(configuration);

        return services;
    }

    private static void AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        var smtpSection = configuration.GetRequiredSection("Smtp");
        services.Configure<SmtpOptions>(options =>
        {
            smtpSection.Bind(options);
            options.Password = configuration["SmtpPassword"]!;
        });
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<INotificationService, EmailNotificationService>();
        services.AddScoped<IEmailTokenService, EmailTokenService>();
    }
}