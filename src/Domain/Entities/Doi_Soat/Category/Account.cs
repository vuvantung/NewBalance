using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Category
{
    public class Account
    {
        public string ACCOUNTUSERNAME { get; set; } = string.Empty;
        public string ACCOUNTPASS { get; set; } = string.Empty;
        public int ACCOUNTADMIN { get; set; }
        public string ACCOUNTNAME { get; set; } = string.Empty;
        public int ACCOUNTPOSTCODE { get; set; }
        public int ACCOUNTTYPE { get; set; }
        public string MUC_HH { get; set; }
        public double MUCGIATRI { get; set; }
        public string VNPE { get; set; } = string.Empty;

    }
}
