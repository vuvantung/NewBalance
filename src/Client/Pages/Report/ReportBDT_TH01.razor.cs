using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Report;
using NewBalance.Client.XLS;
using NewBalance.Domain.Entities.Doi_Soat.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewBalance.Client.Pages.Report
{
    public partial class ReportBDT_TH01
    {
        [Inject] HttpClient _httpClientFilte { get; set; }
        [Inject] IJSRuntime js { get; set; }
        [Inject] private IReportManager _reportManager { get; set; }
        [Parameter] public bool LoadReport { get; set; } = false;
        [Parameter] public int Account { get; set; }
        [Parameter] public DateTime? FromDate { get; set; }
        private string searchString1 = "";
        [Parameter] public DateTime? ToDate { get; set; }
        [Parameter] public EventCallback<bool> IsLoadChanged { get; set; }
        //private string searchString { get; set; } = "";
        private IEnumerable<BDT_TH01> Elements = new List<BDT_TH01>();
        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;
        private BDT_TH01 selectedItem1 = null;
        private HashSet<BDT_TH01> selectedItems = new HashSet<BDT_TH01>();
        protected async override Task OnParametersSetAsync()
        {
            if( LoadReport )
            {
                var fromDateInt = Convert.ToInt32(FromDate.Value.ToString("yyyyMMdd"));
                var toDateInt = Convert.ToInt32(ToDate.Value.ToString("yyyyMMdd"));
                var res = await _reportManager.GetDataBDT_01ReportAsync(Account, fromDateInt, toDateInt);
                if(res.code == "success" )
                {
                    Elements = res.data;
                }
                await IsLoadChanged.InvokeAsync(false);
            }
        }

        private bool FilterFunc1( BDT_TH01 element ) => FilterFunc(element, searchString1);

        private bool FilterFunc( BDT_TH01 element, string searchString )
        {
            if ( string.IsNullOrWhiteSpace(searchString) )
                return true;
            if ( element.DICH_VU.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                return true;
            if ( $"{element.SAN_LUONG} {element.KHOI_LUONG} {element.CUOC_CHINH_CONG_BO} {element.CUOC_DVCT_CONG_BO} {element.TONG_CUOC} {element.GIA_VON_CHUA_VAT} {element.THUE_VAT}".Contains(searchString) )
                return true;
            return false;
        }

        private async void ClickTemplateXLS()
        {
        
            Stream streamTemplate = await _httpClientFilte.GetStreamAsync("xlstemplate/BDT-TH01.xlsx");

            var xls = new Excel();
            await xls.TemplateWeatherForecastAsync(js, streamTemplate, Elements.ToArray(), "report.xlsx");
        }


    }
}
