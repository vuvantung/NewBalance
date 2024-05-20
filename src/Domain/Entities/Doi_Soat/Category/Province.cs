using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Category
{
    public class Province
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        public int PROVINCECODE { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string PROVINCENAME { get; set; } = string.Empty;
        public string DESCRIPTION { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        public int REGIONCODE { get; set; }
        public string PROVINCELISTCODE { get; set; } = string.Empty;
    }
}
