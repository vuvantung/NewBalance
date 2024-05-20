using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Category
{
    public class MapProvinceDistrictCommune
    {
        public int PROVINCECODE { get; set; }
        public string PROVINCENAME { get; set; } = string.Empty;
        public string PROVINCEDES { get; set; } = string.Empty;
        public int REGIONCODE { get; set; }
        public string PROVINCELISTCODE { get; set; } = string.Empty;
        public int DISTRICTCODE { get; set; }
        public string DISTRICTNAME { get; set; } = string.Empty;
        public string DISTRICTDES { get; set; } = string.Empty;
        public string COMMUNECODE { get; set; } = string.Empty;
        public string COMMUNENAME { get; set; } = string.Empty;
        public string POSCODE { get; set; }
        public string POSNAME { get; set; } = string.Empty;
        public string ADDRESS { get; set; }
        public string POSTYPECODE { get; set; }
        public string POSLEVELCODE { get; set; }
        public string UNITCODE { get; set; }
        public string STATUS { get; set; }
        public bool VX { get; set; }
        public bool VXHD { get; set; }
    }
}
