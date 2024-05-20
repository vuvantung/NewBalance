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
        [Required(ErrorMessage = "Trường này không được để trống")]
        [StringLength(6, ErrorMessage = "Không được quá 6 kí tự")]
        public string POSCODE { get; set; }
        [StringLength(100, ErrorMessage = "Không được quá 100 kí tự")]
        public string POSNAME { get; set; } = string.Empty;

        public string ADDRESS { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [StringLength(2, ErrorMessage = "Không được quá 2 kí tự")]
        public string POSTYPECODE { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public int PROVINCECODE { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [StringLength(3, ErrorMessage = "Không được quá 3 kí tự")]
        public string POSLEVELCODE { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [StringLength(6, ErrorMessage = "Không được quá 6 kí tự")]
        public string COMMUNECODE { get; set; }
        [StringLength(6, ErrorMessage = "Không được quá 6 kí tự")]
        public string UNITCODE { get; set; }
        [StringLength(1, ErrorMessage = "Không được quá 1 kí tự")]
        public string STATUS { get; set; }
        public bool VX { get; set; } 
        public bool VXHD { get; set; }
        //public bool isVX { get => isVX; set => isVX = (VX == 1) ? true : false; }
        //public bool isVXHD { get => isVXHD; set => isVXHD = (VXHD == 1) ? true : false; }

    }
}
