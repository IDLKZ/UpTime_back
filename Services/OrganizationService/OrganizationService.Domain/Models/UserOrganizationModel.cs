using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Domain.Models
{
    public class UserOrganizationModel : BaseModel
    {
        public Guid UserId { get; set; }

        public string IIN { get; set; }

        public string Role { get; set; }

        public Guid? OrganizationId { get; set; }
        public OrganizationModel? Organization { get; set; }
        public Guid? BranchId { get; set; }
        public BranchModel? Branch { get; set; }

        public int Status { get; set; }

    }
}
