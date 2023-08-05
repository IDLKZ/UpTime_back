using OrganizationService.Application.DTO.BaseDTO;
using OrganizationService.Application.DTO.BranchDTO;
using OrganizationService.Application.DTO.InfoTypeDTO;
using OrganizationService.Application.DTO.OrganizationDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.DTO.InfoDTO
{
    public class InfoRDTO : BaseRDTO
    {

        public Guid? InfoTypeId { get; set; }
        public InfoTypeRDTO? InfoType { get; set; }

        public Guid? OrganizationId { get; set; }
        public OrganizationRDTO? Organization { get; set; }
        public Guid? BranchId { get; set; }
        public BranchRDTO? Branch { get; set; }

        public string Value { get; set; }
        public int Status { get; set; }
    }
}
