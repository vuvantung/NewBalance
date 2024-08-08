using ClosedXML.Report.Utils;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Client.Extensions;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter;
using NewBalance.Client.Infrastructure.Ultities;
using NewBalance.Client.Shared.Dialogs;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewBalance.Client.Pages.Category.PostalDirectory
{
    public partial class DistrictView
    {
        private IEnumerable<FilterData> _provinceFilter;
        [Inject] private ICategoryManager _categoryManager { get; set; }
        [Inject] private IFilterManager _filterManager { get; set; }
        private string[] provinceList { get; set; }
        private int selectedRowNumber = -1;
        private IEnumerable<District> pagedData;
        private MudTable<District> table;
        private HashSet<District> selectedItems = new HashSet<District>();
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        private District elementBeforeEdit;
        private string CurrentUserId { get; set; }
        private string _account;
        private string _categoryType;
        private bool loadCategory = false;
        private ClaimsPrincipal _currentUser;
        bool _loading = false;
        private string strProvinceCode { get; set; } = "0";
        private string strDistrictCode { get; set; } = "";
        private string strDistrictName { get; set; } = "";
        private async Task FilterSearch()
        {
            _loading = true;
            await table.ReloadServerData();
            _loading = false;
        }
        protected override async Task OnInitializedAsync()
        {

            _currentUser = await _authenticationManager.CurrentUser();

            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if ( user == null ) return;
            if ( user.Identity?.IsAuthenticated == true )
            {
                CurrentUserId = user.GetUserId();
                _provinceFilter = await _filterManager.GetProvinceFilterAsync();
                provinceList = _provinceFilter.Select(x => x.TEXT.ToString()).ToArray();
                
            }
        }
        private async Task<TableData<District>> ServerReload( TableState state )
        {
            ResponseData<District> res = new ResponseData<District>();
            res = await _categoryManager.GetCategoryDistrictAsync_V2(Hepler.GetValueFromTextSelect(strProvinceCode), strDistrictCode, strDistrictName, state.Page, state.PageSize);
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

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            parameters.Add(nameof(AddEditCategoryDistrictModal.provinceList), provinceList);
            if ( id != 0 )
            {
                var _district = pagedData.FirstOrDefault(c => c.DISTRICTCODE == id);
                if ( _district != null )
                {

                    parameters.Add(nameof(AddEditCategoryDistrictModal.AddEditDistrictModel), new District
                    {
                        DISTRICTCODE = _district.DISTRICTCODE,
                        DISTRICTNAME = _district.DISTRICTNAME,
                        DESCRIPTION = _district.DESCRIPTION,
                        PROVINCECODE = _district.PROVINCECODE
                    });
                    parameters.Add(nameof(AddEditCategoryDistrictModal.Title), "Chỉnh sửa");
                    parameters.Add(nameof(AddEditCategoryDistrictModal.ProvinceCode), $"{_district.PROVINCECODE}-{_district.PROVINCENAME}");
                }
            }
            else
            {
                parameters.Add(nameof(AddEditCategoryDistrictModal.Title), "Tạo mới");
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryDistrictModal>(id == 0 ? "Tạo mới" : "Chỉnh sửa", parameters, options);
            var result = await dialog.Result;
            if ( !result.Cancelled )
            {
                await table.ReloadServerData();
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

    }
}
