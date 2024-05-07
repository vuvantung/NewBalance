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
    public partial class CategoryGiaVonChuanNT
    {
        [Inject] private IFilterManager _filterManager { get; set; }
        private IEnumerable<FilterData> _accountFilter;
        private string _account = "0";
        [Inject] private ICategoryManager _categoryManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        private IEnumerable<GiaVonChuanNT> pagedData;
        private MudTable<GiaVonChuanNT> table;
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        private GiaVonChuanNT selectedItem1 = null;
        private List<string> editEvents = new();
        private GiaVonChuanNT elementBeforeEdit;
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

        private async Task<TableData<GiaVonChuanNT>> ServerReload( TableState state )
        {
            var res = await _categoryManager.GetCategoryGiaVonChuanNTAsync(state.Page, state.PageSize, Convert.ToInt32(_account));
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
        private void OnChangeSelect()
        {
            table.ReloadServerData();
        }
        public void Dispose()
        {
            _loaded = false;
        }
        private void AddEditionEvent( string message )
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        private void BackupItem( object element )
        {
            elementBeforeEdit = new()
            {
                MATINH = ((GiaVonChuanNT)element).MATINH,
                DICHVU = ((GiaVonChuanNT)element).DICHVU,
                MADV = ((GiaVonChuanNT)element).MADV,
                PHANLOAI = ((GiaVonChuanNT)element).PHANLOAI,
                TYLEGIAVON = ((GiaVonChuanNT)element).TYLEGIAVON,
                DONVICHIUAP = ((GiaVonChuanNT)element).DONVICHIUAP,
                GHICHU = ((GiaVonChuanNT)element).GHICHU,
                TUNGAY = ((GiaVonChuanNT)element).TUNGAY,
                DENNGAY = ((GiaVonChuanNT)element).DENNGAY,


            };

        }

        private void ItemHasBeenCommitted( object element )
        {
            AddEditionEvent($"RowEditCommit event: Changes to Element {((GiaVonChuanNT)element).MATINH} committed");
        }

        private void ResetItemToOriginalValues( object element )
        {
            ((GiaVonChuanNT)element).MATINH = elementBeforeEdit.MATINH;
            ((GiaVonChuanNT)element).DICHVU = elementBeforeEdit.DICHVU;
            ((GiaVonChuanNT)element).MADV = elementBeforeEdit.MADV;
            ((GiaVonChuanNT)element).PHANLOAI = elementBeforeEdit.PHANLOAI;
            ((GiaVonChuanNT)element).TYLEGIAVON = elementBeforeEdit.TYLEGIAVON;
            ((GiaVonChuanNT)element).DONVICHIUAP = elementBeforeEdit.DONVICHIUAP;
            ((GiaVonChuanNT)element).GHICHU = elementBeforeEdit.GHICHU;
            ((GiaVonChuanNT)element).TUNGAY = elementBeforeEdit.TUNGAY;
            ((GiaVonChuanNT)element).DENNGAY = elementBeforeEdit.DENNGAY;

        }
    }

    
}
