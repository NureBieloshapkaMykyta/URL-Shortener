using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DataAccess;

namespace URL_Shortener.Extensions;

public static class MigrationExtension
{
    public static async Task ApplyMigration(this IApplicationBuilder builder)
    {
        var localScope = builder.ApplicationServices.CreateScope();

        var dbContext = localScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var roleManager = localScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        dbContext.Database.Migrate();
        if (!await roleManager.RoleExistsAsync(AppUserRole.Admin.ToString()))
        {
            var userRoles = Enum.GetValues<AppUserRole>();
            foreach (var userRole in userRoles)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>() { Name = userRole.ToString() });
            }
        }
    }
}
