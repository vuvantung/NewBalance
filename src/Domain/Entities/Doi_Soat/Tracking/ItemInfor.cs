using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Domain.Entities.Doi_Soat.Tracking
{
    public class ItemInfor
    {
        public string ITEMCODE { get; set; } = string.Empty;
        public string ACCEPTANCECOUNTRYCODE { get; set; } = string.Empty;
        public string ACCEPTANCECOUNTRYNAME { get; set; } = string.Empty;
        public string ACCEPTANCEPOSCODE { get; set; } = string.Empty;
        public string ACCEPTANCEPOSNAME { get; set; } = string.Empty;
        public string SENDERFULLNAME { get; set; } = string.Empty;
        public string SENDERADDRESS { get; set; } = string.Empty;
        public string SENDINGCONTENT { get; set; } = string.Empty;
        public string MAINFREIGHT { get; set; } = string.Empty;
        public string FUELSURCHARGEFREIGHT { get; set; } = string.Empty;
        public string FARREGIONFREIGHT { get; set; } = string.Empty;
        public string SUBFREIGHT { get; set; } = string.Empty;
        public string TOTALFREIGHT { get; set; } = string.Empty;
        public string TOTALFREIGHTVAT { get; set; } = string.Empty;
        public string DATACODE { get; set; } = string.Empty;
        public string EXECUTEORDER { get; set; } = string.Empty;
        public string COUNTRYCODE { get; set; } = string.Empty;
        public string COUNTRYNAME { get; set; } = string.Empty;
        public string PROVINCECODE { get; set; } = string.Empty;
        public string PROVINCENAME { get; set; } = string.Empty;
        public string RECEIVERFULLNAME { get; set; } = string.Empty;
        public string RECEIVERADDRESS { get; set; } = string.Empty;
        public string RECEIVERTEL { get; set; } = string.Empty;
        public string WIDTH { get; set; } = string.Empty;
        public string HEIGHT { get; set; } = string.Empty;
        public string LENGTH { get; set; } = string.Empty;
        public string WEIGHT { get; set; } = string.Empty;
        public string WEIGHTCONVERT { get; set; } = string.Empty;
    }
}
