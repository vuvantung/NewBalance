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
using NewBalance.Application.Features.Brands.Commands.AddEdit;
using NewBalance.Client.Pages.Catalog;


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryAccount
    {
        [Inject] private IFilterManager _filterManager { get; set; }
        private IEnumerable<FilterData> _accountFilter;
        private string _account = "0";
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<Account> pagedData;
        private MudTable<Account> table;
        private HashSet<Account> selectedItems = new HashSet<Account>();
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

        private async Task<TableData<Account>> ServerReload( TableState state )
        {
            var res = await _categoryManager.GetCategoryAccountAsync(state.Page, state.PageSize, Convert.ToInt32(_account));
            IEnumerable<Account> data = res.data;

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
                case "ACCOUNTUSERNAME":
                    data = data.OrderByDirection(state.SortDirection, o => o.ACCOUNTUSERNAME);
                    break;
                case "ACCOUNTPASS":
                    data = data.OrderByDirection(state.SortDirection, o => o.ACCOUNTPASS);
                    break;
                case "ACCOUNTADMIN":
                    data = data.OrderByDirection(state.SortDirection, o => o.ACCOUNTADMIN);
                    break;
                case "ACCOUNTNAME":
                    data = data.OrderByDirection(state.SortDirection, o => o.ACCOUNTNAME);
                    break;
                case "ACCOUNTPOSTCODE":
                    data = data.OrderByDirection(state.SortDirection, o => o.ACCOUNTPOSTCODE);
                    break;
                case "ACCOUNTTYPE":
                    data = data.OrderByDirection(state.SortDirection, o => o.ACCOUNTTYPE);
                    break;
                case "MUC_HH":
                    data = data.OrderByDirection(state.SortDirection, o => o.MUC_HH);
                    break;
                case "MUCGIATRI":
                    data = data.OrderByDirection(state.SortDirection, o => o.MUCGIATRI);
                    break;
                case "VNPE":
                    data = data.OrderByDirection(state.SortDirection, o => o.VNPE);
                    break;
            }

            pagedData = data;
            return new TableData<Account>() { TotalItems = totalItems, Items = pagedData };

        }
        private void OnSearch( string text )
        {
            searchString = text;
            table.ReloadServerData();
        }
        private void OnChangeSelect( )
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
            var dialog = _dialogService.Show<AddEditCategoryAccountModal>("Tạo mới",parameters,options: options);
            var result = await dialog.Result;
            if ( !result.Cancelled )
            {
                await table.ReloadServerData();
            }
        }

    }

    
}
