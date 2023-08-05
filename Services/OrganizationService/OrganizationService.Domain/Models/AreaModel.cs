using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Domain.Models
{
    public class AreaModel : BaseModel
    {

        public string TitleRu { get; set; }
        public string TitleKk { get; set; }
        public string TitleEn { get; set; }
        public string Code { get; set; }
        public Polygon? Area { get; set; }

        public int Status { get; set; }

    }
}
