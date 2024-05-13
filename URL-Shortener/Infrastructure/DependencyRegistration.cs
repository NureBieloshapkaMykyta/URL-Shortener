using Application.Abstractions;
using Infrastructure.Extensions;
using Infrastructure.Services.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddJwt(configuration);
        services.AddScoped<ITokenService, JwtService>();
    }
}
