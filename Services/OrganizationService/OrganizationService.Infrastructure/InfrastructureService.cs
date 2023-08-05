using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Infrastructure.Contracts.Repositories;
using OrganizationService.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Infrastructure
{
    public static class InfrastructureService
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MySqlConnectionString");
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


            //Scoped
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<ILegalFormRepository, LegalFormRepository>();
            services.AddScoped<IInfoTypeRepository, InfoTypeRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IInfoRepository, InfoRepository>();
            services.AddScoped<IUserOrganizationRepository, UserOrganizationRepository>();



            return services;

        }


    }
}
