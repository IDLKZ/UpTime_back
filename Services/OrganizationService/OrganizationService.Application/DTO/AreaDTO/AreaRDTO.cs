using NetTopologySuite.Geometries;
using OrganizationService.Application.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.DTO.AreaDTO
{
    public class AreaRDTO : BaseRDTO
    {
        public string TitleRu { get; set; }
        public string TitleKk { get; set; }
        public string TitleEn { get; set; }
        public string Code { get; set; }
        public Polygon? Area { get; set; }

        public int Status { get; set; }
    }
}
