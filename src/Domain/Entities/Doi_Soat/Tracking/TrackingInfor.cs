using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Tracking
{
    public class TrackingInfor
    {
        public ItemInfor ItemInfor { get; set; } = new ItemInfor();
        public IEnumerable<ServiceInfor> ServiceInfor { get; set; } = new List<ServiceInfor>();
        public IEnumerable<AffairInfor> AffairInfor { get; set; } = new List<AffairInfor>();
        public IEnumerable<StatusTrace> StatusTrace { get; set; } = new List<StatusTrace>();
        public IEnumerable<DeliveryStatus> DeliveryStatus { get; set; } = new List<DeliveryStatus>();

    }
}
