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
    public partial class CategoryGiaVonChuan
    {
        [Parameter] public int Account { get; set; } = 0;
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<GiaVonChuan> pagedData;
        private MudTable<GiaVonChuan> table;
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

        private async Task<TableData<GiaVonChuan>> ServerReload( TableState state )
        {
            var res = await _categoryManager.GetCategoryGiaVonChuanAsync(state.Page, state.PageSize, Account);
            IEnumerable<GiaVonChuan> data = res.data;

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
                case "ACCOUNT":
                    data = data.OrderByDirection(state.SortDirection, o => o.ACCOUNT);
                            break; 
                case "NOITINH":
                   data = data.OrderByDirection(state.SortDirection, o => o.NOITINH);
                           break; 
                case "ETLNV":
                   data = data.OrderByDirection(state.SortDirection, o => o.ETLNV);
                           break; 
                case "ETLLV":
                   data = data.OrderByDirection(state.SortDirection, o => o.ETLLV);
                           break; 
                case "EHNNV":
                   data = data.OrderByDirection(state.SortDirection, o => o.EHNNV);
                           break; 
                case "EHNLV":
                   data = data.OrderByDirection(state.SortDirection, o => o.EHNLV);
                           break; 
                case "ENNNV":
                   data = data.OrderByDirection(state.SortDirection, o => o.ENNNV);
                           break; 
                case "ENNLV":
                   data = data.OrderByDirection(state.SortDirection, o => o.ENNLV);
                           break; 
                case "LONV":
                   data = data.OrderByDirection(state.SortDirection, o => o.LONV);
                           break; 
                case "LOLV":
                   data = data.OrderByDirection(state.SortDirection, o => o.LOLV);
                           break; 
                case "PHTNT":
                   data = data.OrderByDirection(state.SortDirection, o => o.PHTNT);
                           break; 
                case "PHTLT":
                   data = data.OrderByDirection(state.SortDirection, o => o.PHTLT);
                           break; 
                case "THOATHUAN":
                   data = data.OrderByDirection(state.SortDirection, o => o.THOATHUAN);
                           break; 
                case "TTB":
                   data = data.OrderByDirection(state.SortDirection, o => o.TTB);
                           break; 
                case "TTC":
                   data = data.OrderByDirection(state.SortDirection, o => o.TTC);
                           break; 
                case "TTV":
                   data = data.OrderByDirection(state.SortDirection, o => o.TTV);
                           break; 
                case "ECT":
                   data = data.OrderByDirection(state.SortDirection, o => o.ECT);
                           break; 
                case "QUOCTE":
                   data = data.OrderByDirection(state.SortDirection, o => o.QUOCTE);
                           break; 
                case "TUNGAY":
                   data = data.OrderByDirection(state.SortDirection, o => o.TUNGAY);
                           break; 
                case "DENNGAY":
                    data = data.OrderByDirection(state.SortDirection, o => o.DENNGAY);
                            break;

            }

        pagedData = data;
            return new TableData<GiaVonChuan>() { TotalItems = totalItems, Items = pagedData };

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
