using OrganizationService.Application.DTO.BaseDTO;
using OrganizationService.Application.DTO.BranchDTO;
using OrganizationService.Application.DTO.OrganizationDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.DTO.UserOrganizationDTO
{
    public class UserOrganizationRDTO : BaseRDTO
    {
        public Guid UserId { get; set; }

        public string IIN { get; set; }

        public string Role { get; set; }

        public Guid? OrganizationId { get; set; }
        public OrganizationRDTO? Organization { get; set; }
        public Guid? BranchId { get; set; }
        public BranchRDTO? Branch { get; set; }

        public int Status { get; set; }




    }
}
