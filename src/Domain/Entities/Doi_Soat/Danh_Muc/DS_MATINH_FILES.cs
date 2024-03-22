using NewBalance.Domain.Contracts;
using NewBalance.Domain.Entities.ExtendedAttributes;
using System;

namespace NewBalance.Domain.Entities.Doi_Soat.Danh_Muc
{
    public class DS_MATINH_FILES : AuditableEntity<int>
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