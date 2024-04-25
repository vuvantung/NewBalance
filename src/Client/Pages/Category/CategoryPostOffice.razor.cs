using MudBlazor;
using System.Collections.Generic;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using System;


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryPostOffice
    {
        [Parameter] public int Account { get; set; } = 0;
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<PostOffice> pagedData;
        private MudTable<PostOffice> table;
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        protected async override Task OnParametersSetAsync()
        {
            if( _loaded )
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
            var res = await _categoryManager.GetCategoryPostOfficeAsync(state.Page, state.PageSize, Account);
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
        public void Dispose()
        {
            _loaded = false;
        }
    }

    
}
