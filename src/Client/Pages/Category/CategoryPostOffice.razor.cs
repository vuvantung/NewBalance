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


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryPostOffice
    {
        [Inject] private IFilterManager _filterManager { get; set; }
        private IEnumerable<FilterData> _accountFilter;
        private string _account = "0";
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<PostOffice> pagedData;
        private HashSet<PostOffice> selectedItems = new HashSet<PostOffice>();
        private MudTable<PostOffice> table;
        private int totalItems;
        private bool _loaded = true;
        private string searchString = null;

        protected override async Task OnInitializedAsync()
        {
            _loaded = true;
        }


        private async Task<TableData<PostOffice>> ServerReload( TableState state )
        {
            var res = await _categoryManager.GetCategoryPostOfficeAsync(state.Page, state.PageSize, Convert.ToInt32(_account));
            IEnumerable<PostOffice> data = res.data;

            //data = data.Where(element =>
            //{
            //    if ( string.IsNullOrWhiteSpace(searchString) )
            //        return true;
            //    if ( element.Sign.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
            //        return true;
            //    if ( element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) )
            //        return true;
            //    if ( $"{element.Number} {element.Position} {element.Molar}".Contains(searchString) )
            //        return true;
            //    return false;
            //}).ToArray();
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
            var parameters = new DialogParameters();

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryPostOfficeModal>("Tạo mới", parameters, options: options);
            var result = await dialog.Result;
            if ( !result.Cancelled )
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
            await _categoryManager.UpdateCategoryAsync(request);
            await table.ReloadServerData();
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
            await _categoryManager.UpdateCategoryAsync(request);
            await table.ReloadServerData();
        }

    }

    
}
