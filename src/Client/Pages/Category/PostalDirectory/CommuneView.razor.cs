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
    public partial class CommuneView
    {
        private IEnumerable<FilterData> _provinceFilter;
        private IEnumerable<FilterData> _districtFilter;
        [Inject] private ICategoryManager _categoryManager { get; set; }
        [Inject] private IFilterManager _filterManager { get; set; }
        private string[] provinceList { get; set; }
        private string[] districtList { get; set; }
        private int selectedRowNumber = -1;
        private IEnumerable<Commune> pagedData;
        private MudTable<Commune> table;
        private HashSet<Commune> selectedItems = new HashSet<Commune>();
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
        private string strDistrictCode { get; set; } = "0";
        private string strCommuneCode { get; set; } = "";
        private string strCommuneName { get; set; } = "";


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

        private async Task ChangeProvince(string str)
        {
            strProvinceCode = str;
            _districtFilter = await _filterManager.GetDistrictFilterAsync(Hepler.GetValueFromTextSelect(strProvinceCode));
            districtList = _districtFilter.Select(x => x.TEXT.ToString()).ToArray();
        }

        private async Task<TableData<Commune>> ServerReload( TableState state )
        {
            ResponseData<Commune> res = new ResponseData<Commune>();
            res = await _categoryManager.GetCategoryCommuneAsync_V2(Hepler.GetValueFromTextSelect(strProvinceCode), Hepler.GetValueFromTextSelect(strDistrictCode), strCommuneCode, strCommuneName, state.Page, state.PageSize);
            IEnumerable<Commune> data = res.data;
            data = data.Where(element =>
            {
                if ( string.IsNullOrWhiteSpace(searchString) )
                    return true;
                if ( element.COMMUNECODE.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.COMMUNENAME.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.DISTRICTCODE.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                if ( element.DISTRICTNAME.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
                    return true;
                return false;
            }).ToArray();
            totalItems = res.total;
            switch ( state.SortLabel )
            {
                case "COMMUNECODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.COMMUNECODE);
                    break;
                case "COMMUNENAME":
                    data = data.OrderByDirection(state.SortDirection, o => o.COMMUNENAME);
                    break;
                case "DISTRICTCODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.DISTRICTCODE);
                    break;
                case "DISTRICTNAME":
                    data = data.OrderByDirection(state.SortDirection, o => o.DISTRICTNAME);
                    break;

            }

            pagedData = data;
            return new TableData<Commune>() { TotalItems = totalItems, Items = pagedData };

        }

        private async Task InvokeModal(string id = "0")
        {
            var parameters = new DialogParameters();
            parameters.Add(nameof(AddEditCategoryCommuneModal.provinceList), provinceList);
            if ( id != "0" )
            {
                var _commune = pagedData.FirstOrDefault(c => c.COMMUNECODE == id);
                if ( _commune != null )
                {

                    parameters.Add(nameof(AddEditCategoryCommuneModal.AddEditCommuneModel), new Commune
                    {
                        COMMUNECODE = _commune.COMMUNECODE,
                        COMMUNENAME = _commune.COMMUNENAME,
                        EmailModified = _currentUser.GetEmail()
                    });
                    parameters.Add(nameof(AddEditCategoryCommuneModal.Title), "Chỉnh sửa");
                    parameters.Add(nameof(AddEditCategoryCommuneModal.ProvinceCode), $"{_commune.PROVINCECODE}-{_commune.PROVINCENAME}");
                    parameters.Add(nameof(AddEditCategoryCommuneModal.DistrictCode), $"{_commune.DISTRICTCODE}-{_commune.DISTRICTNAME}");
                }
            }
            else
            {
                parameters.Add(nameof(AddEditCategoryCommuneModal.Title), "Tạo mới");
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryCommuneModal>(id == "0" ? "Tạo mới" : "Chỉnh sửa", parameters, options);
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
                    TABLENAME = "Commune",
                    IDCOLUMNNAME = "COMMUNECODE",
                    IDCOLUMNVALUE = item.COMMUNECODE
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
        private void OnSearch( string text )
        {
            searchString = text;
            table.ReloadServerData();
        }
    }
}
