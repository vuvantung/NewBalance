using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Category
{
    public class Commune
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string COMMUNECODE { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string COMMUNENAME { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string DISTRICTCODE { get; set; } = string.Empty;
        public string DISTRICTNAME { get; set; } = string.Empty;
        public string? PROVINCECODE { get; set; } = string.Empty;
        public string? PROVINCENAME { get; set; } = string.Empty;
        public string EmailModified { get; set; } = string.Empty;
    }
}
