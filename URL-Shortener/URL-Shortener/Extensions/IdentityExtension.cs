using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.DataAccess;

namespace URL_Shortener.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole<Guid>>(options => 
        {
            options.User.RequireUniqueEmail = false;
        })
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}
