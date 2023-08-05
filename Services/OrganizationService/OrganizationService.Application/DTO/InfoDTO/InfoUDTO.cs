using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.DTO.InfoDTO
{
    public class InfoUDTO
    {
        public Guid? InfoTypeId { get; set; }

        public Guid? OrganizationId { get; set; }
        public Guid? BranchId { get; set; }

        public string Value { get; set; }
        public int Status { get; set; }
    }
}
