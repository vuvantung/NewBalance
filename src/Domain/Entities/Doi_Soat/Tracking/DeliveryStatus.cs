using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Tracking
{
    public class DeliveryStatus
    {
        public string NGAY_GIO_PHAT { get; set; } = string.Empty;
        public string NGAY_NHAP_TT_PHAT { get; set; } = string.Empty;
        public string BUU_CUC_PHAT { get; set; } = string.Empty;
        public string LY_DO { get; set; } = string.Empty;
        public string THOI_GIAN_CAP_NHAT { get; set; } = string.Empty;
        public string TUYEN_PHAT_BUU_TA { get; set; } = string.Empty;
    }
}
