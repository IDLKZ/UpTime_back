using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Domain.Models
{
    public class BranchModel : BaseModel
    {
        public Guid? OrganizationId { get; set; }
        public OrganizationModel? Organization { get; set; }
        public Guid? AreaId { get; set; }
        public AreaModel? Area { get; set; }
        public string TitleRu { get; set; }
        public string TitleKk { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionRu { get; set; }
        public string? DescriptionKk { get; set; }
        public string? DescriptionEn { get; set; }

        public string AddressRu { get; set; }
        public string? AddressKk { get; set; }
        public string? AddressEn { get; set; }

        public Point? Point { get; set; }
        public int Status { get; set; }

        public string Code { get; set; }



    }
}
