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
using NewBalance.Client.Shared.Dialogs;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Shared.Wrapper;
using System.Security.Claims;
using NewBalance.Client.Extensions;


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryCommune
    {
        [Parameter] public bool IsForPostOfficeCategory { get; set; } = false;
        [Parameter] public int DistrictCode { get; set; } = 0;
        [Parameter] public string DistrictName { get; set; } = string.Empty;
        [Parameter] public EventCallback<(int CommuneCode, string CommuneName)> DataChangedCommune { get; set; }
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<Commune> pagedData;
        private MudTable<Commune> table;
        private HashSet<Commune> selectedItems = new HashSet<Commune>();
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        private ClaimsPrincipal _currentUser;
        protected async override Task OnParametersSetAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            if ( _loaded )
            {
                table.ReloadServerData();
                
            }
            else
            {
                _loaded = true;
            }
            
        }

        private async Task<TableData<Commune>> ServerReload( TableState state )
        {
            ResponseData<Commune> res = new ResponseData<Commune>();
            if(! IsForPostOfficeCategory )
            {
                res = await _categoryManager.GetCategoryCommuneAsync(state.Page, state.PageSize, DistrictCode);
            }
            else
            {
                res = await _categoryManager.GetCategoryCommuneAsync(0, 9999, DistrictCode);
            }
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
            parameters.Add(nameof(AddEditCategoryCommuneModal.AddEditCommuneModel), new Commune
            {
                DISTRICTCODE = DistrictCode.ToString(),
                EmailModified = _currentUser.GetEmail()
            }); ;
;
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryCommuneModal>("Tạo mới", parameters, options: options);
            var result = await dialog.Result;

            if ( result.Data.AsBool() == true )
            {
                await table.ReloadServerData();
            }
        }

        private async Task InvokeModalDelete()
        {
            var request = new List<SingleUpdateRequest>();
            foreach(var item in selectedItems )
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

        private async Task RowClickEventCommune( TableRowClickEventArgs<Commune> tableRowClickEventArgs )
        {
            var communeCode = Convert.ToInt32(tableRowClickEventArgs.Item.COMMUNECODE);
            var communeName = tableRowClickEventArgs.Item.COMMUNENAME.Trim();
            var newData = (communeCode, communeName);
            await DataChangedCommune.InvokeAsync(newData);
        }
    }

    
}
