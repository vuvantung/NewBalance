using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Category
{
    public class DM_Dich_Vu
    {
       
        [Required]
        public string DICHVU { get; set; }
        [Required]
        public string TENDICHVU { get; set; } = string.Empty;
        public string PHANLOAI { get; set; } = string.Empty;
        public string GHICHU { get; set; } = string.Empty;
        public string  ACCOUNT { get; set; } = string.Empty;
    }
}
