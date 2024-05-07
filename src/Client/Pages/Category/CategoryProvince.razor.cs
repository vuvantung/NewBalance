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


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryProvince
    {
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private int selectedRowNumber = -1;
        private int ProvinceCode { get; set; } = 0;
        private string ProvinceName { get; set; } = string.Empty;
        private int DistrictCode { get; set; } = 0;
        private string DistrictName { get; set; } = string.Empty;
        private IEnumerable<Province> pagedData;
        private MudTable<Province> table;
        private HashSet<Province> selectedItems = new HashSet<Province>();
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        protected override async Task OnInitializedAsync()
        {
            _loaded = true;
        }

        private async Task<TableData<Province>> ServerReload( TableState state )
        {
            var res = await _categoryManager.GetCategoryProvinceAsync(state.Page, state.PageSize);
            IEnumerable<Province> data = res.data;

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
            if ( !result.Cancelled )
            {
                await table.ReloadServerData();
            }
        }

        private void HandleDataDistrict( (int DistrictCodeCB, string DistrictNameCB) data )
        {
            DistrictCode = data.DistrictCodeCB;
            DistrictName = data.DistrictNameCB;
        }

        private void RowClickEvent( TableRowClickEventArgs<Province> tableRowClickEventArgs )
        {
            ProvinceCode = tableRowClickEventArgs.Item.PROVINCECODE;
            ProvinceName = tableRowClickEventArgs.Item.PROVINCENAME.Trim();
            DistrictCode = 0;
            DistrictName = "";
        }

        private string SelectedRowClassFunc( Province element, int rowNumber )
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
