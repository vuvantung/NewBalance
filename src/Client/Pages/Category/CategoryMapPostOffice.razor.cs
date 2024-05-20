using MudBlazor;
using System.Collections.Generic;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using System;
using static MudBlazor.CategoryTypes;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter;
using ClosedXML.Report.Utils;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Client.Shared.Dialogs;
using Microsoft.JSInterop;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO;
using System.Net.Security;


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryMapPostOffice
    {
        [Inject] private ICategoryManager _categoryManager { get; set; }
        [Inject] private IJSRuntime _jsRunTime { get; set; }
        private int selectedRowNumber = -1;
        private int ProvinceCode { get; set; } = 0;
        private string ProvinceName { get; set; } = string.Empty;
        private int CommuneCode { get; set; } = 0;
        private string CommuneName { get; set; } = string.Empty;
        private int DistrictCode { get; set; } = 0;
        private string DistrictName { get; set; } = string.Empty;
        private IEnumerable<Province> pagedData;
        private MudTable<Province> table;
        private HashSet<Province> selectedItems = new HashSet<Province>();
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        protected override async Task OnInitializedAsync()
        {
            _loaded = true;
        }

        private async Task<TableData<Province>> ServerReload( TableState state )
        {
            var res = await _categoryManager.GetCategoryProvinceAsync(0, 99999);
            IEnumerable<Province> data = res.data;

            data = data.Where(element =>
            {
                if ( string.IsNullOrWhiteSpace(searchString) )
                    return true;
                if ( element.PROVINCENAME.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.DESCRIPTION.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.PROVINCELISTCODE.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( $"{element.PROVINCECODE} {element.REGIONCODE}".Contains(searchString) )
                    return true;
                return false;
            }).ToArray();
            totalItems = res.total;
            switch ( state.SortLabel )
            {
                case "PROVINCECODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.PROVINCECODE);
                    break;
                case "PROVINCENAME":
                    data = data.OrderByDirection(state.SortDirection, o => o.PROVINCENAME);
                    break;
                
            }

            pagedData = data;
            return new TableData<Province>() { TotalItems = totalItems, Items = pagedData };

        }
        private void OnSearch( string text )
        {
            searchString = text;
            table.ReloadServerData();
        }

        public void Dispose()
        {
            _loaded = false;
        }
        private async Task InvokeModal()
        {
            if ( ProvinceCode == 0 )
            {
                _snackBar.Add($"Chưa chọn tỉnh", Severity.Error);
            }
            else if ( DistrictCode == 0 )
            {
                _snackBar.Add($"Chưa chọn quận/huyệnh", Severity.Error);
            }
            else if ( CommuneCode == 0 )
            {
                _snackBar.Add($"Chưa chọn phường/xã", Severity.Error);
            }
            else
            {
                var parameters = new DialogParameters();
                parameters.Add(nameof(AddEditCategoryPostOfficeModal.AddEditPostOfficeModel), new PostOffice
                {
                    PROVINCECODE = ProvinceCode,
                    UNITCODE = DistrictCode.ToString(),
                    COMMUNECODE = CommuneCode.ToString()
                });
                var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
                var dialog = _dialogService.Show<AddEditCategoryPostOfficeModal>("Tạo mới", parameters, options: options);
                var result = await dialog.Result;
                if ( !result.Cancelled )
                {
                    await table.ReloadServerData();
                }
            }

        }

        private void HandleDataDistrict( (int DistrictCodeCB, string DistrictNameCB) data )
        {
            DistrictCode = data.DistrictCodeCB;
            DistrictName = data.DistrictNameCB;
        }

        private void HandleDataCommune( (int CommuneCodeCB, string CommuneNameCB) data )
        {
            CommuneCode = data.CommuneCodeCB;
            CommuneName = data.CommuneNameCB;
        }


        private void RowClickEvent( TableRowClickEventArgs<Province> tableRowClickEventArgs )
        {
            ProvinceCode = tableRowClickEventArgs.Item.PROVINCECODE;
            ProvinceName = tableRowClickEventArgs.Item.PROVINCENAME.Trim();
            DistrictCode = 0;
            DistrictName = "";
            CommuneCode = 0;
            CommuneName = "";
        }

        private async Task InvokeModalDelete()
        {
            var request = new List<SingleUpdateRequest>();
            foreach ( var item in selectedItems )
            {
                request.Add(new SingleUpdateRequest
                {
                    TABLENAME = "PROVINCE",
                    IDCOLUMNNAME = "PROVINCECODE",
                    IDCOLUMNVALUE = item.PROVINCECODE.ToString(),
                });
            }
            var parameters = new DialogParameters<DeleteDialog>();
            parameters.Add(x => x.ContentText, "Các phường xã đã chọn sẽ bị xóa!");
            parameters.Add(x => x.data, request);
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteDialog>("Xóa", parameters, options: options);
            var result = await dialog.Result;

            if ( result.Data.AsBool() == true )
            {
                await table.ReloadServerData();
            }
        }

        private string SelectedRowClassFunc( Province element, int rowNumber )
        {
            if ( selectedRowNumber == rowNumber )
            {
                selectedRowNumber = -1;
                return string.Empty;
            }
            else if ( table.SelectedItem != null && table.SelectedItem.Equals(element) )
            {
                selectedRowNumber = rowNumber;
                return "selected";
            }
            else
            {
                return string.Empty;
            }
        }

        private async Task ExportToExcel()
        {
            var parameters = new DialogParameters<Loading>();
            parameters.Add(x => x.ContentText, "Xin vui lòng đợi");
            var options = new DialogOptions {MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = await _dialogService.ShowAsync<Loading>("Xóa", parameters, options: options);
            try
            {
                var listExport = await _categoryManager.GetAllCategoryProvinceDistrictCommuneAsync();
                if ( listExport.code == "success" )
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using ( var excelPackage = new ExcelPackage() )
                    {
                        // Add a worksheet to the Excel package
                        var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                        // Add headers to the worksheet
                        string[] headers = {
                        "Mã tỉnh", "Tên tỉnh", "Mô tả tỉnh", "Mã khu vực", "Đầu mã chấp nhận",
                        "Mã quận/huyện", "Tên quận/huyện", "Mô tả quận huyện", "Mã phường/xã",
                        "Tên phường/xã", "Mã bưu cục", "Tên bưu cục", "Địa chỉ bưu cục",
                        "Loại bưu cục", "Cấp bưu cục", "Mã đơn vị", "Trạng thái", "Vùng xa", "Vùng xa/Hải đạo"
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


                        using ( var range = worksheet.Cells["A1:S1"] )
                        {
                            // Set PatternType
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            // Set Màu cho Background
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Orange);
                            // Canh giữa cho các text
                            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            // Set Font cho text  trong Range hiện tại
                            range.Style.Font.SetFromFont("Arial", 11, true, false, false, false);
                            // Set Border
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                            // Set màu ch Border
                            range.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Blue);
                        }
                        //// generate file and save to disk
                        var fileStream = new MemoryStream();
                        await excelPackage.SaveAsAsync(fileStream);
                        var fileName = "Danh mục quận huyện phường xã.xlsx";
                        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        var fileBytes = fileStream.ToArray();

                        //// download file
                        await _jsRunTime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes), mimeType);
                        _snackBar.Add("Xuất file thành công", Severity.Success);
                    }
                }
            }
            catch (Exception ex )
            {
                _snackBar.Add($"Xuất file thất bại: {ex.Message}", Severity.Error);
            }
           
            dialog.Close(); 
        }
    }

    
}
