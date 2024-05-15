using Application;
using Infrastructure;
using Persistence;
using URL_Shortener.Extensions;
using URL_Shortener.MapperProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddAutoMapper(cfg => 
{
    cfg.AddProfile<AppUserProfile>();
    cfg.AddProfile<UrlProfile>();
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

await app.ApplyMigration();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
