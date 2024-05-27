using MudBlazor;
using System.Collections.Generic;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using System;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
using NewBalance.Application.Features.Doi_Soat;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewBalance.Client.Shared.Dialogs;
using ClosedXML.Report.Utils;


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryPostOffice
    {
        [Parameter] public int ProvinceCode { get; set; } = 0;
        [Parameter] public int DistrictCode { get; set; } = 0;
        [Parameter] public int CommuneCode { get; set; } = 0;
        [Parameter] public string CommuneName { get; set; } = string.Empty;
        [Parameter] public int ContainVXHD { get; set; } = 0;
        [Parameter] public bool IsLargeCategory { get; set; } = true;
        private int lastDistrict { get;set; } = 0;
        [Inject] private IFilterManager _filterManager { get; set; }
        private IEnumerable<FilterData> _accountFilter;
        private string _account = "0";
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<PostOffice> pagedData;
        private HashSet<PostOffice> selectedItems = new HashSet<PostOffice>();
        private MudTable<PostOffice> table;
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        private PostOffice elementBeforeEdit;

        protected async override Task OnParametersSetAsync()
        {
            if ( _loaded )
            {
                await table.ReloadServerData();
            }
            else
            {
                _loaded = true;
            }

        }


        private async Task<TableData<PostOffice>> ServerReload( TableState state )
        {
            if( IsLargeCategory )
            {
                if ( DistrictCode != lastDistrict ) CommuneCode = 0;
            }
            var res = await _categoryManager.GetCategoryPostOfficeAsync(state.Page, state.PageSize, ProvinceCode, DistrictCode, CommuneCode, ContainVXHD);
            lastDistrict = DistrictCode;
            IEnumerable<PostOffice> data = res.data;

            data = data.Where(element =>
            {
                if ( string.IsNullOrWhiteSpace(searchString) )
                    return true;
                if ( element.POSCODE.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.POSNAME.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.ADDRESS.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.POSTYPECODE.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.POSLEVELCODE.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.COMMUNECODE.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.UNITCODE.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.STATUS.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( $"{element.PROVINCECODE}".Contains(searchString) )
                    return true;
                return false;
            }).ToArray();
            totalItems = res.total;
            switch ( state.SortLabel )
            {
                case "POSCODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.POSCODE);
                            break; 
                case "POSNAME":
                    data = data.OrderByDirection(state.SortDirection, o => o.POSNAME);
                            break; 
                case "ADDRESS":
                    data = data.OrderByDirection(state.SortDirection, o => o.ADDRESS);
                            break; 
                case "POSTYPECODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.POSTYPECODE);
                            break; 
                case "PROVINCECODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.PROVINCECODE);
                            break; 
                case "POSLEVELCODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.POSLEVELCODE);
                            break; 
                case "COMMUNECODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.COMMUNECODE);
                            break; 
                case "UNITCODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.UNITCODE);
                            break; 
                case "STATUS":
                    data = data.OrderByDirection(state.SortDirection, o => o.STATUS);
                            break; 
                case "VX":
                    data = data.OrderByDirection(state.SortDirection, o => o.VX);
                            break; 
                case "VXHD":
                    data = data.OrderByDirection(state.SortDirection, o => o.VXHD);
                            break; 
            }

        pagedData = data;
            return new TableData<PostOffice>() { TotalItems = totalItems, Items = pagedData };

        }
        private void OnSearch( string text )
        {
            searchString = text;
            table.ReloadServerData();
        }

        private void OnChangeSelect()
        {
            table.ReloadServerData();
        }
        public void Dispose()
        {
            _loaded = false;
        }
        private async Task InvokeModal()
        {
            if( ProvinceCode == 0 )
            {
                _snackBar.Add($"Chưa chọn tỉnh", Severity.Error);
            }
            else if ( DistrictCode == 0 )
            {
                _snackBar.Add($"Chưa chọn quận/huyệnh", Severity.Error);
            }
            else if (CommuneCode == 0 )
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

        private async Task InvokeModalDelete()
        {
            var request = new List<SingleUpdateRequest>();
            foreach ( var item in selectedItems )
            {
                request.Add(new SingleUpdateRequest
                {
                    TABLENAME = "POS_TEMP",
                    IDCOLUMNNAME = "POSCODE",
                    IDCOLUMNVALUE = item.POSCODE.ToString(),
                });
            }
            var parameters = new DialogParameters<DeleteDialog>();
            parameters.Add(x => x.ContentText, "Các bưu cục đã chọn sẽ bị xóa!");
            parameters.Add(x => x.data, request);
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteDialog>("Xóa", parameters, options: options);
            var result = await dialog.Result;

            if ( result.Data.AsBool() == true )
            {
                await table.ReloadServerData();
            }
        }

        private async Task HandleCheckedChangedVX (PostOffice item, bool isChecked )
        {
            var request = new SingleUpdateRequest
            {
                TABLENAME = "POS_TEMP",
                IDCOLUMNNAME = "POSCODE",
                IDCOLUMNVALUE = item.POSCODE,
                CHANGECOLUMNNAME = "VX",
                CHANGECOLUMNVALUE = (isChecked == true) ? "1" : "0"
            };

            var parameters = new DialogParameters();
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<CofirmDialog>("Cập nhật dữ liệu", parameters, options: options);
            var result = await dialog.Result;

            if ( !result.Cancelled )
            {
                var res = await _categoryManager.UpdateCategoryAsync(request);
                if ( res.code == "SUCESSS" )
                {
                    await table.ReloadServerData();
                    _snackBar.Add($"Cập nhật thành công", Severity.Success);
                }
                else
                {
                    _snackBar.Add($"Lỗi xử lý", Severity.Error);
                }
            }
            else
            {
                _snackBar.Add($"Giữ nguyên thay đổi", Severity.Info);
            }
            
            
        }

        private async Task HandleCheckedChangedHD( PostOffice item, bool isChecked )
        {
            var request = new SingleUpdateRequest
            {
                TABLENAME = "POS_TEMP",
                IDCOLUMNNAME = "POSCODE",
                IDCOLUMNVALUE = item.POSCODE,
                CHANGECOLUMNNAME = "VXHD",
                CHANGECOLUMNVALUE = (isChecked == true) ? "1" : "0"
            };
            var parameters = new DialogParameters();
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<CofirmDialog>("Cập nhật dữ liệu", parameters, options: options);
            var result = await dialog.Result;

            if ( !result.Cancelled )
            {
                var res = await _categoryManager.UpdateCategoryAsync(request);
                if(res.code == "SUCESSS" )
                {
                    await table.ReloadServerData();
                    _snackBar.Add($"Cập nhật thành công", Severity.Success);
                }
                else
                {
                    _snackBar.Add($"Lỗi xử lý", Severity.Error);
                }
                
            }
            else
            {
                _snackBar.Add($"Giữ nguyên thay đổi", Severity.Info);
            }
           
        }


        private void BackupItem( object element )
        {
            elementBeforeEdit = new()
            {
                POSCODE = ((PostOffice)element).POSCODE,
                POSNAME = ((PostOffice)element).POSNAME,
                ADDRESS = ((PostOffice)element).ADDRESS,
                POSTYPECODE = ((PostOffice)element).POSTYPECODE,
                PROVINCECODE = ((PostOffice)element).PROVINCECODE,
                POSLEVELCODE = ((PostOffice)element).POSLEVELCODE,
                COMMUNECODE = ((PostOffice)element).COMMUNECODE,
                UNITCODE = ((PostOffice)element).UNITCODE,
                VX = ((PostOffice)element).VX,
                VXHD = ((PostOffice)element).VXHD,
            };

        }

        private async void ItemHasBeenCommitted( object element )
        {
            var request = new PostOffice
            {
                POSCODE = ((PostOffice)element).POSCODE,
                POSNAME = ((PostOffice)element).POSNAME,
                ADDRESS = ((PostOffice)element).ADDRESS,
                POSTYPECODE = ((PostOffice)element).POSTYPECODE,
                PROVINCECODE = ((PostOffice)element).PROVINCECODE,
                POSLEVELCODE = ((PostOffice)element).POSLEVELCODE,
                COMMUNECODE = ((PostOffice)element).COMMUNECODE,
                UNITCODE = ((PostOffice)element).UNITCODE,
                VX = ((PostOffice)element).VX,
                VXHD = ((PostOffice)element).VXHD
            };
            var response = await _categoryManager.UpdatePostOfficeAsync(request);
            if ( response.code == "SUCCESS" )
            {
                _snackBar.Add("Cập nhật bưu cục thành công", Severity.Success);
            }
            else
            {
                _snackBar.Add(response.message, Severity.Error);
            }
        }
        
        private void ResetItemToOriginalValues( object element )
        {
            ((PostOffice)element).POSCODE = elementBeforeEdit.POSCODE;
            ((PostOffice)element).POSNAME = elementBeforeEdit.POSNAME;
            ((PostOffice)element).ADDRESS = elementBeforeEdit.ADDRESS;
            ((PostOffice)element).POSTYPECODE = elementBeforeEdit.POSTYPECODE;
            ((PostOffice)element).PROVINCECODE = elementBeforeEdit.PROVINCECODE;
            ((PostOffice)element).POSLEVELCODE = elementBeforeEdit.POSLEVELCODE;
            ((PostOffice)element).COMMUNECODE = elementBeforeEdit.COMMUNECODE;
            ((PostOffice)element).UNITCODE = elementBeforeEdit.UNITCODE;
            ((PostOffice)element).VX = elementBeforeEdit.VX;
            ((PostOffice)element).VXHD = elementBeforeEdit.VXHD;
        }

    }

    
}
