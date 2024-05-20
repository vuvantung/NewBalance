﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Category
{
    public class Account
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string ACCOUNTUSERNAME { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string ACCOUNTPASS { get; set; } = string.Empty;
        public int ACCOUNTADMIN { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string ACCOUNTNAME { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        public int ACCOUNTPOSTCODE { get; set; }
        public int ACCOUNTTYPE { get; set; }
        public string MUC_HH { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public double MUCGIATRI { get; set; }
        public string VNPE { get; set; } = string.Empty;

    }
}
