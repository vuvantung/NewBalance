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
    public partial class CategoryGiaVonChuanNT
    {
        [Parameter] public int Account { get; set; } = 0;
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<GiaVonChuanNT> pagedData;
        private MudTable<GiaVonChuanNT> table;
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

        private async Task<TableData<GiaVonChuanNT>> ServerReload( TableState state )
        {
            var res = await _categoryManager.GetCategoryGiaVonChuanNTAsync(state.Page, state.PageSize, Account);
            IEnumerable<GiaVonChuanNT> data = res.data;

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
                case "MATINH":
                    data = data.OrderByDirection(state.SortDirection, o => o.MATINH);
                            break;
                case "DICHVU":
                   data = data.OrderByDirection(state.SortDirection, o => o.DICHVU);
                           break;
                case "MADV":
                   data = data.OrderByDirection(state.SortDirection, o => o.MADV);
                           break;
                case "PHANLOAI":
                   data = data.OrderByDirection(state.SortDirection, o => o.PHANLOAI);
                           break;
                case "TYLEGIAVON":
                   data = data.OrderByDirection(state.SortDirection, o => o.TYLEGIAVON);
                           break;
                case "DONVICHIUAP":
                   data = data.OrderByDirection(state.SortDirection, o => o.DONVICHIUAP);
                           break;
                case "GHICHU":
                   data = data.OrderByDirection(state.SortDirection, o => o.GHICHU);
                           break;
                case "TUNGAY":
                   data = data.OrderByDirection(state.SortDirection, o => o.TUNGAY);
                           break;
                case "DENNGAY":
                    data = data.OrderByDirection(state.SortDirection, o => o.DENNGAY);
                            break;
            }

        pagedData = data;
            return new TableData<GiaVonChuanNT>() { TotalItems = totalItems, Items = pagedData };

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
