using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.DTO.UserOrganizationDTO
{
    public class UserOrganizationUDTO
    {
        public Guid UserId { get; set; }

        public string IIN { get; set; }

        public string Role { get; set; }

        public Guid? OrganizationId { get; set; }
        public Guid? BranchId { get; set; }

        public int Status { get; set; }
    }
}
