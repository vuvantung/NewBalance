using ClosedXML.Report.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using NewBalance.Application.Features.Brands.Commands.AddEdit;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Client.Extensions;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using NewBalance.Client.Pages.Catalog;
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
    public partial class ProvinceView
    {

        private IEnumerable<FilterData> _accountFilter;
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private int selectedRowNumber = -1;
        private IEnumerable<Province> pagedData;
        private MudTable<Province> table;
        private HashSet<Province> selectedItems = new HashSet<Province>();
        IEnumerable<Province> data { get; set; }
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        private Province elementBeforeEdit;
        private string CurrentUserId { get; set; }
        private string _account;
        private string _categoryType;
        private bool loadCategory = false;
        private ClaimsPrincipal _currentUser;
        bool _loading = false;
        private string strProvinceCode { get; set; } = "";
        private string strProvinceName { get; set; } = "";

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
            }
        }

        private void HandleLoadCategory()
        {
            loadCategory = true;
        }
        private async Task<TableData<Province>> ServerReload( TableState state )
        {
            var res = await _categoryManager.GetCategoryProvinceAsync_V2(strProvinceCode, strProvinceName, state.Page, state.PageSize);
            data = res.data;

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
                case "DESCRIPTION":
                    data = data.OrderByDirection(state.SortDirection, o => o.DESCRIPTION);
                    break;
                case "REGIONCODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.REGIONCODE);
                    break;
                case "PROVINCELISTCODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.PROVINCELISTCODE);
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
            var parameters = new DialogParameters();

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryProvinceModal>("Tạo mới", parameters, options: options);
            var result = await dialog.Result;
            if ( result.Data.AsBool() == true )
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
                    TABLENAME = "PROVINCE",
                    IDCOLUMNNAME = "PROVINCECODE",
                    IDCOLUMNVALUE = item.PROVINCECODE.ToString(),
                });
            }
            var parameters = new DialogParameters<DeleteDialog>();
            parameters.Add(x => x.ContentText, "Các tỉnh, thành phố đã chọn sẽ bị xóa!");
            parameters.Add(x => x.data, request);
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteDialog>("Xóa", parameters, options: options);
            var result = await dialog.Result;

            if ( result.Data.AsBool() == true )
            {
                await table.ReloadServerData();
            }
        }

        private async Task InvokeModal(  int id = 0 )
        {
            var parameters = new DialogParameters();
            if ( id != 0 )
            {
                var _province = data.FirstOrDefault(c => c.PROVINCECODE == id);
                if ( _province != null )
                {

                    parameters.Add(nameof(AddEditCategoryProvinceModal.AddEditProvinceModel), new Province
                    {
                        PROVINCECODE = _province.PROVINCECODE,
                        PROVINCENAME = _province.PROVINCENAME,
                        DESCRIPTION = _province.DESCRIPTION,
                        REGIONCODE = _province.REGIONCODE,
                        PROVINCELISTCODE = _province.PROVINCELISTCODE
                    });
                    parameters.Add(nameof(AddEditCategoryProvinceModal.Title), "Tạo mới");
                }
            }
            else
            {
                parameters.Add(nameof(AddEditCategoryProvinceModal.Title), "Tạo mới");
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryProvinceModal>(id == 0 ? "Tạo mới" : "Chỉnh sửa", parameters, options);
            var result = await dialog.Result;
            if ( !result.Cancelled )
            {
                await table.ReloadServerData();
            }
        }
    }
}
