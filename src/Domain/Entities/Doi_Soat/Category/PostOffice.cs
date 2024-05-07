﻿using System;
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
        public int MABC { get; set; }
        [Required]
        public string TENBC { get; set; }
        public string MADV { get; set; }
        [Required]
        public int KHUVUC { get; set; }
        public int PLDUONGTHU { get; set; }
        [Required]
        public int MABC_GOC { get; set; }
        public int TRUYENDL { get; set; }
        public int HUONG { get; set; }
        public string MADV_GOC { get; set; }
        [Required]
        public int ACCOUNT { get; set; }
        public int LUU_KHO { get; set; }
        public int TTAM { get; set; }
    }
}
