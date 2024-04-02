using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.ExportCasReport
{
    public class InforFileCasReport
    {
        public int ID { get; set; }
        public int MATINH { get; set; }
        public string TENTINH { get; set; } = string.Empty;
        public int TONGSO { get; set; }
        public int TUNGAY { get; set; }
        public int DENNGAY { get; set; }
    }
}
