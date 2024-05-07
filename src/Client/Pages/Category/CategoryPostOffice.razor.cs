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
        private bool _loaded;
        private string searchString = null;
        protected async override Task OnParametersSetAsync()
        {
            _accountFilter = await _filterManager.GetAccountFilterAsync();
            if ( _loaded )
            {
                table.ReloadServerData();
            }
            else
            {
                _loaded = true;
            }
            
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
                case "MABC":
                    data = data.OrderByDirection(state.SortDirection, o => o.MABC);
                            break; 
                case "TENBC":
                    data = data.OrderByDirection(state.SortDirection, o => o.TENBC);
                            break; 
                case "MADV":
                    data = data.OrderByDirection(state.SortDirection, o => o.MADV);
                            break; 
                case "KHUVUC":
                    data = data.OrderByDirection(state.SortDirection, o => o.KHUVUC);
                            break; 
                case "PLDUONGTHU":
                    data = data.OrderByDirection(state.SortDirection, o => o.PLDUONGTHU);
                            break; 
                case "MABC_GOC":
                    data = data.OrderByDirection(state.SortDirection, o => o.MABC_GOC);
                            break; 
                case "TRUYENDL":
                    data = data.OrderByDirection(state.SortDirection, o => o.TRUYENDL);
                            break; 
                case "HUONG":
                    data = data.OrderByDirection(state.SortDirection, o => o.HUONG);
                            break; 
                case "MADV_GOC":
                    data = data.OrderByDirection(state.SortDirection, o => o.MADV_GOC);
                            break; 
                case "ACCOUNT":
                    data = data.OrderByDirection(state.SortDirection, o => o.ACCOUNT);
                            break; 
                case "LUU_KHO":
                    data = data.OrderByDirection(state.SortDirection, o => o.LUU_KHO);
                            break; 
                case "TTAM":
                    data = data.OrderByDirection(state.SortDirection, o => o.TTAM);
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
    }

    
}
