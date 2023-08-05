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
    public class LegalFormRepository : GenericRepository<LegalFormModel>, ILegalFormRepository
    {
        public LegalFormRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
