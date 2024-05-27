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
using NewBalance.Application.Features.Products.Commands.AddEdit;
using NewBalance.Client.Pages.Catalog;
using NewBalance.Domain.Entities.Catalog;
using ClosedXML.Report.Utils;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Client.Shared.Dialogs;


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryDistrict
    {
        [Parameter] public bool IsForPostOfficeCategory { get; set; } = false;
        [Parameter] public int ProvinceCode { get; set; } = 0;
        [Parameter] public string ProvinceName { get; set; } = string.Empty;
        [Parameter] public EventCallback<(int DistrictCode, string DistrictName)> DataChangedDistrict { get; set; }
        private int selectedRowNumber = -1;
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<District> pagedData;
        private MudTable<District> table;
        private HashSet<District> selectedItems = new HashSet<District>();
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        private bool isEditting = false;
        private District elementBeforeEdit;
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

        private async Task<TableData<District>> ServerReload( TableState state )
        {
            ResponseData<District> res = new ResponseData<District>();
            if ( !IsForPostOfficeCategory )
            {
                res = await _categoryManager.GetCategoryDistrictAsync(state.Page, state.PageSize, ProvinceCode);
            }
            else
            {
                res = await _categoryManager.GetCategoryDistrictAsync(0, 9999, ProvinceCode);
            }
            IEnumerable<District> data = res.data;
            data = data.Where(element =>
            {
                if ( string.IsNullOrWhiteSpace(searchString) )
                    return true;
                if ( element.PROVINCENAME.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.DESCRIPTION.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.DISTRICTNAME.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( $"{element.PROVINCECODE} {element.DISTRICTCODE}".Contains(searchString) )
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
                case "DESCRIPTION":
                    data = data.OrderByDirection(state.SortDirection, o => o.DESCRIPTION);
                    break;
                case "DISTRICTCODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.DISTRICTCODE);
                    break;
                case "DISTRICTNAME":
                    data = data.OrderByDirection(state.SortDirection, o => o.DISTRICTNAME);
                    break;
                
            }

            pagedData = data;
            return new TableData<District>() { TotalItems = totalItems, Items = pagedData };

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
            var parameters = new DialogParameters();
            parameters.Add(nameof(AddEditCategoryDistrictModal.AddEditDistrictModel), new District
            {
                PROVINCECODE = ProvinceCode
            });
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryDistrictModal>("Tạo mới", parameters, options: options);
            var result = await dialog.Result;
            if ( result.Data.AsBool() == true )
            {
                await table.ReloadServerData();
            }
        }

        private async Task RowClickEventDistrict( TableRowClickEventArgs<District> tableRowClickEventArgs )
        {
            if ( !isEditting )
            {
                var districtCode = tableRowClickEventArgs.Item.DISTRICTCODE;
                var districtName = tableRowClickEventArgs.Item.DISTRICTNAME.Trim();
                var newData = (districtCode, districtName);
                await DataChangedDistrict.InvokeAsync(newData);
            }
            
        }

        private async Task InvokeModalDelete()
        {
            var request = new List<SingleUpdateRequest>();
            foreach ( var item in selectedItems )
            {
                request.Add(new SingleUpdateRequest
                {
                    TABLENAME = "District",
                    IDCOLUMNNAME = "DISTRICTCODE",
                    IDCOLUMNVALUE = item.DISTRICTCODE.ToString(),
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

        private string SelectedRowClassFuncDistrict( District element, int rowNumber )
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

        private void BackupItem( object element )
        {
            elementBeforeEdit = new()
            {
                DISTRICTCODE = ((District)element).DISTRICTCODE,
                DISTRICTNAME = ((District)element).DISTRICTNAME,
                DESCRIPTION = ((District)element).DESCRIPTION,
                PROVINCECODE = ((District)element).PROVINCECODE,
                PROVINCENAME = ((District)element).PROVINCENAME
            };
            isEditting = true;
        }

        private async void ItemHasBeenCommitted( object element )
        {
            var request = new District
            {
                DISTRICTCODE = ((District)element).DISTRICTCODE,
                DISTRICTNAME = ((District)element).DISTRICTNAME,
                DESCRIPTION = ((District)element).DESCRIPTION,
                PROVINCECODE = ((District)element).PROVINCECODE,
                PROVINCENAME = ((District)element).PROVINCENAME
            };
            var response = await _categoryManager.UpdateDistrictAsync(request);
            if ( response.code == "SUCCESS" )
            {
                _snackBar.Add("Cập nhật thành công", Severity.Success);
            }
            else
            {
                _snackBar.Add(response.message, Severity.Error);
            }
            isEditting = false;
        }

        private void ResetItemToOriginalValues( object element )
        {
            ((District)element).DISTRICTCODE = elementBeforeEdit.DISTRICTCODE;
            ((District)element).DISTRICTNAME = elementBeforeEdit.DISTRICTNAME;
            ((District)element).DESCRIPTION = elementBeforeEdit.DESCRIPTION;
            ((District)element).PROVINCECODE = elementBeforeEdit.PROVINCECODE;
            ((District)element).PROVINCENAME = elementBeforeEdit.PROVINCENAME;
            isEditting = false;
        }

    }

    
}
