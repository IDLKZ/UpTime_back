using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Domain.Models
{
    public class InfoModel : BaseModel
    {
        public Guid? InfoTypeId { get; set; }
        public InfoTypeModel? InfoType { get; set; }

        public Guid? OrganizationId { get; set; }
        public OrganizationModel? Organization { get; set; }
        public Guid? BranchId { get; set; }
        public BranchModel? Branch { get; set; }

        public string Value { get; set; }
        public int Status { get; set; }


    }
}
