using Application.Abstractions;
using Application.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUrlService, UrlService>();

        return services;
    }
}
