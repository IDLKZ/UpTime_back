using Duende.IdentityServer.Services;
using IdentiyService.Database.Seeder;
using IdentiyService.Database;
using IdentiyService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentiyService.Services;
using IdentiyService.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentiyService.Extnesions
{
    public static class AppExtension
    {
        public static IServiceCollection AddAppExtension(this IServiceCollection services, IConfiguration configuration)
        {
            //Database Services
            string connectionString = configuration.GetConnectionString("MySqlConnectionString");
            services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            //Scoped
            services.AddScoped<IDbSeeder, DbSeeder>();
            services.AddScoped<IProfileService, ProfileService>();
            //Razor
            services.AddRazorPages();
            //AddIdentity
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseErrorEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
               .AddInMemoryIdentityResources(IdentityConfiguration.GetIdentityResources)
               .AddInMemoryApiScopes(IdentityConfiguration.GetApiScopes)
               .AddInMemoryClients(IdentityConfiguration.GetClients)
               .AddAspNetIdentity<ApplicationUser>()
               .AddProfileService<ProfileService>()
               .AddDeveloperSigningCredential();
            return services;
        }



    }
}
