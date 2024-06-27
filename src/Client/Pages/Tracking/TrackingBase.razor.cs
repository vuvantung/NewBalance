using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NewBalance.Application.Requests.Doi_soat;
using NewBalance.Client.Extensions;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Tracking;
using NewBalance.Client.Shared.Dialogs;
using NewBalance.Client.XLS;
using NewBalance.Domain.Entities.Doi_Soat.Tracking;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.Generic;
using NewBalance.Application.Features.Doi_Soat;
using System.Linq;
using Microsoft.JSInterop;

namespace NewBalance.Client.Pages.Tracking
{
    public partial class TrackingBase
    {
        [Inject] HttpClient _httpClientFilte { get; set; }
        [Inject] private IJSRuntime _jsRunTime { get; set; }
        [Inject] private ITrackingManager _trackingManager { get; set; }
        public bool _loading { get; set; } = false;
        private int _typeTracking { get; set; } = 1;
        private TrackingInfor TrackingInfor { get; set; } = new TrackingInfor();

        private string ItemCode { get; set; }  = string.Empty;
        private string ListItemCode { get; set; }  = string.Empty;
        private ClaimsPrincipal _currentUser;
        protected async override Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();       
        }
        private async Task Tracking()
        {

            var res = await _trackingManager.TrackingItem(ItemCode.Trim().ToUpper());
            TrackingInfor = res.data;
            StateHasChanged();
            
        }

        private async Task TrackingSLL()
        {

            string[] trackingNumbersArray = ListItemCode.Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            // Create the root element
            var sessionId = DateTime.Now.ToString("yyyyMMddHHmmss");
            XElement documentElement = new XElement("DOCUMENTELEMENT");

            // Create TRACK elements and add them directly to DOCUMENTELEMENT
            foreach ( string trackingNumber in trackingNumbersArray )
            {
                XElement trackElement = new XElement("TRACK",
                    new XElement("SH_BG", trackingNumber),
                    new XElement("IDSESSION", sessionId),
                    new XElement("IDUSER", _currentUser.GetUserId())
                );

                // Add all child elements to the TRACK element at once
                documentElement.Add(trackElement);
                
            }

            // Create XDocument and add the root element
            XDocument xmlDocument = new XDocument(documentElement);

            // Output the XML
            string xmlString = xmlDocument.ToString();
            var response = await _trackingManager.TrackingSLL(new TrackingSLLRequest
            {
                SessionID = $"{sessionId}{_currentUser.GetUserId()}",
                XmlData = xmlString
            });
            await ExportToExcel(response);

        }

        private async Task ExportToExcel( ResponseData<LastStatusItem> listExport )
        {
            var parameters = new DialogParameters<Loading>();
            parameters.Add(x => x.ContentText, "Xin vui lòng đợi");
            var options = new DialogOptions { MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = await _dialogService.ShowAsync<Loading>("Xóa", parameters, options: options);
            try
            {

                if ( listExport.code == "success" )
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using ( var excelPackage = new ExcelPackage() )
                    {
                        // Add a worksheet to the Excel package
                        var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                        // Add headers to the worksheet
                        string[] headers = {
                        "Số hiệu BG", "Số hiệu của KH", "Người nhận", "Bưu cục chấp nhận", "Ngày chấp nhận",
                        "Kết quả phát (Lần 1)", "Ngày phát (Lần 1)", "Ngày nhập phát (Lần 1)", "Người nhận/Lý do (Lần 1)",
                        "BC phát (Lần 1)", "Kết quả phát (Lần 2)", "Ngày phát (Lần 2)", "Ngày nhập phát (Lần 2)",
                        "Người nhận/Lý do (Lần 2)", "BC phát (Lần 2)", "Kết quả phát (Hiện tại)", "Ngày phát (Hiện tại)", "Ngày nhập phát (Hiện tại)", "Người nhận/Lý do (Hiện tại)",
                        "BC phát (Hiện tại)", "Trạng thái cuối cùng", "Vị trí cuối cùng", "Địa chỉ", "Ngày chuyển hoàn", "Ngày nộp tiền", "Ngày trả tiền"
                    };
                        for ( int col = 1; col <= headers.Length; col++ )
                        {
                            worksheet.Cells[1, col].Value = headers[col - 1];
                        }

                        // Batch size for data insertion
                        int batchSize = 3000;
                        int row = 2; // Start from the second row for data

                        // Loop through the data in batches
                        for ( int i = 0; i < listExport.data.Count; i += batchSize )
                        {
                            int remainingCount = Math.Min(batchSize, listExport.data.Count - i);

                            // Define the range for this batch
                            var range = worksheet.Cells[row, 1, row + remainingCount - 1, headers.Length];

                            // Get the data for this batch
                            var batchData = listExport.data.Skip(i).Take(remainingCount).ToList();

                            // Fill the range with data
                            range.LoadFromCollection(batchData);

                            row += remainingCount;
                        }


                        using ( var range = worksheet.Cells["A1:Z1"] )
                        {
                            // Set PatternType
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            // Set Màu cho Background
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Orange);
                            // Canh giữa cho các text
                            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            // Set Font cho text  trong Range (Hiện tại)
                            range.Style.Font.SetFromFont("Arial", 11, true, false, false, false);
                            // Set Border
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                            // Set màu ch Border
                            range.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Blue);
                        }
                        //// generate file and save to disk
                        var fileStream = new MemoryStream();
                        await excelPackage.SaveAsAsync(fileStream);
                        var fileName = "Danh sách tra cứu bưu gửi.xlsx";
                        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        var fileBytes = fileStream.ToArray();

                        //// download file
                        await _jsRunTime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes), mimeType);
                        _snackBar.Add("Xuất file thành công", Severity.Success);
                    }
                }
            }
            catch ( Exception ex )
            {
                _snackBar.Add($"Xuất file thất bại: {ex.Message}", Severity.Error);
            }

            dialog.Close();
        }
    }
}
