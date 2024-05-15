using Application.Abstractions;
using Infrastructure.Extensions;
using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddJwt(configuration);
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.AddScoped<ITokenService, JwtService>();

        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
