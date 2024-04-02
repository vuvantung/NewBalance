using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.ExportCasReport
{
    public class DetailCasReport
    {
        public string MAE1 { get; set; } = string.Empty;
        public int NGAYPHATHANH { get; set; }
        public string MABCNHAN { get; set; } = string.Empty;
        public string MABCTRA { get; set; } = string.Empty;
        public string MAKH { get; set; } = string.Empty;
        public string BATCHCODE { get; set; } = string.Empty;
        public int VAN_DON_CHU { get; set; }
        public string MADVCHINH { get; set; } = string.Empty;
        public string MADVCT { get; set; } = string.Empty;
        public int KHOILUONG { get; set; }
        public int KLQD { get; set; }
        public int EDI { get; set; }
        public int PPVX { get; set; }
        public int PPXD { get; set; }
        public int PPHK { get; set; }
        public int CUOCCS { get; set; }
        public int CUOCDVCT { get; set; }
        public decimal TYLEGIAVON { get; set; }
        public int GIAVON { get; set; }
        public decimal TYLEGIAVONDVCTDACBIET { get; set; }
        public int GIAVONDVCTDACBIET { get; set; }
        public int Thu_Lao_Cong_Phat { get; set; }
        public int Cuoc_Chuyen_Hoan { get; set; }
        public int Hoan_Cuoc { get; set; }
        public string Trang_Thai { get; set; } = string.Empty;
    }
}
