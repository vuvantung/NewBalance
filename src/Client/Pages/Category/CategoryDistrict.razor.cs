﻿using MudBlazor;
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
using NewBalance.Application.Features.Products.Commands.AddEdit;
using NewBalance.Client.Pages.Catalog;
using NewBalance.Domain.Entities.Catalog;


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryDistrict
    {
        [Parameter] public int ProvinceCode { get; set; } = 0;
        [Parameter] public string ProvinceName { get; set; } = string.Empty;
        [Parameter] public EventCallback<(int DistrictCode, string DistrictName)> DataChangedDistrict { get; set; }
        private int selectedRowNumber = -1;
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<District> pagedData;
        private MudTable<District> table;
        private HashSet<District> selectedItems = new HashSet<District>();
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        protected async override Task OnParametersSetAsync()
        {
            if ( _loaded )
            {
                await table.ReloadServerData();
            }
            else
            {
                _loaded = true;
            }
            
        }

        private async Task<TableData<District>> ServerReload( TableState state )
        {
            var res = await _categoryManager.GetCategoryDistrictAsync(state.Page, state.PageSize, ProvinceCode);
            IEnumerable<District> data = res.data;

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
                case "DISTRICTCODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.DISTRICTCODE);
                    break;
                case "DISTRICTNAME":
                    data = data.OrderByDirection(state.SortDirection, o => o.DISTRICTNAME);
                    break;
                
            }

            pagedData = data;
            return new TableData<District>() { TotalItems = totalItems, Items = pagedData };

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
            parameters.Add(nameof(AddEditCategoryDistrictModal.AddEditDistrictModel), new District
            {
                PROVINCECODE = ProvinceCode
            });
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryDistrictModal>("Tạo mới", parameters, options: options);
            var result = await dialog.Result;
            if ( !result.Cancelled )
            {
                await table.ReloadServerData();
            }
        }

        private async Task RowClickEventDistrict( TableRowClickEventArgs<District> tableRowClickEventArgs )
        {
            var districtCode = tableRowClickEventArgs.Item.DISTRICTCODE;
            var districtName = tableRowClickEventArgs.Item.DISTRICTNAME.Trim();
            var newData = (districtCode, districtName);
            await DataChangedDistrict.InvokeAsync(newData);
        }

        private string SelectedRowClassFuncDistrict( District element, int rowNumber )
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