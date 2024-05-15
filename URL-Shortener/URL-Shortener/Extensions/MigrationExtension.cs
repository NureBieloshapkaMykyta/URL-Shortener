using Application.Abstractions;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DataAccess;
using URL_Shortener.Helpers;

namespace URL_Shortener.Extensions;

public static class MigrationExtension
{
    public static async Task ApplyMigration(this IApplicationBuilder builder)
    {
        var localScope = builder.ApplicationServices.CreateScope();

        var dbContext = localScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();

        var roleManager = localScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        var alhInfoRepos = localScope.ServiceProvider.GetRequiredService<IRepository<AlhoritmInfo>>();
        var baseAlhoritm = await alhInfoRepos.GetAllAsync(a=>a.Name==BaseAlhoritmConstants.Name);

        if (baseAlhoritm.Data!=null && !baseAlhoritm.Data.Any())
        {
            await alhInfoRepos.AddItemAsync(new AlhoritmInfo() { Name = BaseAlhoritmConstants.Name, Description = BaseAlhoritmConstants.Description });
        }

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
