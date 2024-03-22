using System;
using System.Collections.Generic;

namespace NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll
{
    public class GetAllDS_MATINH_FILESResponse
    {
        public int ID { get; set; }
        public int MATINH { get; set; }
        public string TENTINH { get; set; }
        public int THANG { get; set; }
        public DateTime? DATECREATED { get; set; }
        public DateTime? DATEUPDATED { get; set; }
        public string LINKS { get; set; }
        public int STATUS { get; set; }
        public string CREATEBY { get; set; }
        public string NOTES { get; set;}
    }
}