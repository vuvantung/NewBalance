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
using NewBalance.Client.Shared.Dialogs;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Shared.Wrapper;
using System.Security.Claims;
using NewBalance.Client.Extensions;

namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryDM_Dich_Vu
    {
      
        [Inject] private ICategoryManager _categoryManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Parameter] public string DICHVU { get; set; }
        private List<string> editEvents = new();
        private IEnumerable<DM_Dich_Vu> pagedData;
        private MudTable<DM_Dich_Vu> table;
        private HashSet<DM_Dich_Vu> selectedItems = new HashSet<DM_Dich_Vu>();
        private int totalItems;
        private bool _loaded;
        private string _account = "0";
        private string searchString = null;
        private DM_Dich_Vu elementBeforeEdit;
        private ClaimsPrincipal _currentUser;



        protected async override Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _loaded = true;
           
        }
        private async Task<TableData<DM_Dich_Vu>> ServerReload(TableState state)
        {
            var res = await _categoryManager.GetCategoryDM_Dich_VuAsync(state.Page, state.PageSize, Convert.ToInt32(_account));
            IEnumerable<DM_Dich_Vu> data = res.data;

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
            switch (state.SortLabel)
            {
                
                case "DICHVU":
                    data = data.OrderByDirection(state.SortDirection, o => o.DICHVU);
                    break;
                case "TENDICHVU":
                    data = data.OrderByDirection(state.SortDirection, o => o.TENDICHVU);
                    break;
                case "PHANLOAI":
                    data = data.OrderByDirection(state.SortDirection, o => o.PHANLOAI);
                    break;
                case "GHICHU":
                    data = data.OrderByDirection(state.SortDirection, o => o.GHICHU);
                    break;
                
            }

            pagedData = data;
            return new TableData<DM_Dich_Vu>() { TotalItems = totalItems, Items = pagedData };

        }

         private async Task GetDM_Dich_VusAsync()
        {
            table.ReloadServerData();
        }


        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }

        public void Dispose()
        {
            _loaded = false;
        }

        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        private async Task InvokeModal()
        {
            var parameters = new DialogParameters();
            parameters.Add(nameof(AddEditCategoryDM_Dich_VuModal.AddEditDM_Dich_VuModel), new DM_Dich_Vu
            {
                DICHVU = DICHVU,
                ACCOUNT = _currentUser.GetEmail()
            });
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryDM_Dich_VuModal>("Tạo mới", parameters, options: options);
            var result = await dialog.Result;

            if (result.Data.AsBool() == true)
            {
                await table.ReloadServerData();
            }
        }

        private void BackupItem(object element)
        {
            elementBeforeEdit = new()
            {
                DICHVU = ((DM_Dich_Vu)element).DICHVU,
                TENDICHVU = ((DM_Dich_Vu)element).TENDICHVU,
                PHANLOAI = ((DM_Dich_Vu)element).PHANLOAI,
                GHICHU = ((DM_Dich_Vu)element).GHICHU,


            };
            AddEditionEvent($"RowEditCommit event: Changes to Element {((DM_Dich_Vu)element).ACCOUNT} committed");
        }
        private void ItemHasBeenCommitted(object element )
        {
            AddEditionEvent($"RowEditCommit event: Changes to Element {((DM_Dich_Vu)element).ACCOUNT} committed");
            
        }   

        private void ResetItemToOriginalValues(object element)
        {
            ((DM_Dich_Vu)element).DICHVU = elementBeforeEdit.DICHVU;
            ((DM_Dich_Vu)element).TENDICHVU = elementBeforeEdit.TENDICHVU;
            ((DM_Dich_Vu)element).PHANLOAI = elementBeforeEdit.PHANLOAI;
            ((DM_Dich_Vu)element).GHICHU = elementBeforeEdit.GHICHU;
            AddEditionEvent($"RowEditCommit event: Changes to Element {((DM_Dich_Vu)element).ACCOUNT} committed");
        }
        private async Task InvokeModalDelete()
        {
            var request = new List<SingleUpdateRequest>();
            foreach (var item in selectedItems)
            {
                request.Add(new SingleUpdateRequest
                {
                    TABLENAME = "DM_DICHVUCOBAN",
                    IDCOLUMNNAME = "DICHVU",
                    IDCOLUMNVALUE = item.DICHVU
                });
            }
            var parameters = new DialogParameters<DeleteDialog>();
            parameters.Add(x => x.ContentText, "Các dịch vụ  đã chọn sẽ bị xóa!");
            parameters.Add(x => x.data, request);
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteDialog>("Xóa", parameters, options: options);
            var result = await dialog.Result;

            if (result.Data.AsBool() == true)
            {
                await table.ReloadServerData();
            }
        }
    }
}
