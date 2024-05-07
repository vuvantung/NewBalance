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


namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryCommune
    {
        [Parameter] public int DistrictCode { get; set; } = 0;
        [Parameter] public string DistrictName { get; set; } = string.Empty;
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private IEnumerable<Commune> pagedData;
        private MudTable<Commune> table;
        private HashSet<Commune> selectedItems = new HashSet<Commune>();
        private int totalItems;
        private bool _loaded;
        private string searchString = null;
        protected async override Task OnParametersSetAsync()
        {
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
            var res = await _categoryManager.GetCategoryCommuneAsync(state.Page, state.PageSize, DistrictCode);
            IEnumerable<Commune> data = res.data;

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
                DISTRICTCODE = DistrictCode.ToString()
            });
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryCommuneModal>("Tạo mới", parameters, options: options);
            var result = await dialog.Result;

            if ( result.Data.AsBool() == true )
            {
                await table.ReloadServerData();
            }
        }
    }

    
}
