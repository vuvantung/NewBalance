using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Category
{
    public class PostOffice
    {
        [Required]
        [StringLength(6)]
        public string POSCODE { get; set; }
        [StringLength(100)]
        public string POSNAME { get; set; } = string.Empty;

        public string ADDRESS { get; set; }
        [Required]
        [StringLength(2)]
        public string POSTYPECODE { get; set; }
        [Required]
        [StringLength(3)]
        public int PROVINCECODE { get; set; }
        [Required]
        [StringLength(3)]
        public string POSLEVELCODE { get; set; }
        [Required]
        [StringLength(6)]
        public string COMMUNECODE { get; set; }
        [StringLength(6)]
        public string UNITCODE { get; set; }
        [StringLength(1)]
        public string STATUS { get; set; }
        
        public bool VX { get; set; } 
        public bool VXHD { get; set; }
        //public bool isVX { get => isVX; set => isVX = (VX == 1) ? true : false; }
        //public bool isVXHD { get => isVXHD; set => isVXHD = (VXHD == 1) ? true : false; }

    }
}
