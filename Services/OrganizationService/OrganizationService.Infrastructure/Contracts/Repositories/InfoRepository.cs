using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Domain.Models;
using OrganizationService.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Infrastructure.Contracts.Repositories
{
    public class InfoRepository : GenericRepository<InfoModel>, IInfoRepository
    {
        public InfoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
