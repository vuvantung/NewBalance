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
using System.Security.Claims;
using NewBalance.Client.Extensions;
using ClosedXML.Report.Utils;
using NewBalance.Client.Pages.Identity;
using NewBalance.Application.Features.Doi_Soat;
using NewBalance.Client.Shared.Dialogs;

namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryDM_GiaVonChuan
    {

        [Inject] private ICategoryManager _categoryManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Parameter] public int ACCOUNT { get; set; }
        private List<string> editEvents = new();
        private IEnumerable<GiaVonChuan> pagedData;
        private MudTable<GiaVonChuan> table;
        private HashSet<GiaVonChuan> selectedItems = new HashSet<GiaVonChuan>();
        private int totalItems;
        private bool _loaded;
        private string _account = "0";
        private string searchString = null;
        private GiaVonChuan elementBeforeEdit;
        private ClaimsPrincipal _currentUser;
        private IEnumerable<FilterData> _accountFilter;


        protected async override Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _loaded = true;

        }
        private async Task<TableData<GiaVonChuan>> ServerReload(TableState state)
        {
            var res = await _categoryManager.GetCategoryGiaVonChuanAsync(state.Page, state.PageSize, Convert.ToInt32(_account));
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
            switch (state.SortLabel)
            {
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

        private async Task GetGiaVonChuansAsync()
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

        //private void OnChangeSelect()
        //{
        //    table.ReloadServerData();
        //}

        private async Task InvokeModal()
        {
            var parameters = new DialogParameters();
            parameters.Add(nameof(AddEditCategoryGiaVonChuanModal.AddEditGiaVonChuanModel), new GiaVonChuan
            {
                ACCOUNT = ACCOUNT,
                ACCOUNTEMAIL = _currentUser.GetEmail()
            });
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCategoryGiaVonChuanModal>("Tạo mới", parameters, options: options);
            var result = await dialog.Result;

            if (result.Data.AsBool() == true)
            {
                await table.ReloadServerData();
            }
        }

        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        private void BackupItem(object element)
        {
            elementBeforeEdit = new()
            {
                ACCOUNT = ((GiaVonChuan)element).ACCOUNT,
                NOITINH = ((GiaVonChuan)element).NOITINH,
                ETLNV = ((GiaVonChuan)element).ETLNV,
                ETLLV = ((GiaVonChuan)element).ETLLV,
                EHNNV = ((GiaVonChuan)element).EHNNV,
                EHNLV = ((GiaVonChuan)element).EHNLV,
                ENNNV = ((GiaVonChuan)element).ENNNV,
                ENNLV = ((GiaVonChuan)element).ENNLV,
                LONV = ((GiaVonChuan)element).LONV,
                LOLV = ((GiaVonChuan)element).LOLV,
                PHTNT = ((GiaVonChuan)element).PHTNT,
                PHTLT = ((GiaVonChuan)element).PHTLT,
                THOATHUAN = ((GiaVonChuan)element).THOATHUAN,
                TTB = ((GiaVonChuan)element).TTB,
                TTC = ((GiaVonChuan)element).TTC,
                TTV = ((GiaVonChuan)element).TTV,
                ECT = ((GiaVonChuan)element).ECT,
                QUOCTE = ((GiaVonChuan)element).QUOCTE,
                TUNGAY = ((GiaVonChuan)element).TUNGAY,
                DENNGAY = ((GiaVonChuan)element).DENNGAY

            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((GiaVonChuan)element).ACCOUNTEMAIL}");
        }

        private void ItemHasBeenCommitted(object element)
        {
            AddEditionEvent($"RowEditCommit event: Changes to Element {((GiaVonChuan)element).ACCOUNTEMAIL} committed");
        }

        private void ResetItemToOriginalValues(object element)
        {
            ((GiaVonChuan)element).ACCOUNT = elementBeforeEdit.ACCOUNT;
            ((GiaVonChuan)element).NOITINH = elementBeforeEdit.NOITINH;
            ((GiaVonChuan)element).ETLNV = elementBeforeEdit.ETLNV;
            ((GiaVonChuan)element).ETLLV = elementBeforeEdit.ETLLV;
            ((GiaVonChuan)element).EHNNV = elementBeforeEdit.EHNNV;
            ((GiaVonChuan)element).EHNLV = elementBeforeEdit.EHNLV;
            ((GiaVonChuan)element).ENNNV = elementBeforeEdit.ENNNV;
            ((GiaVonChuan)element).ENNLV = elementBeforeEdit.ENNLV;
            ((GiaVonChuan)element).LONV = elementBeforeEdit.LONV;
            ((GiaVonChuan)element).LOLV = elementBeforeEdit.LOLV;
            ((GiaVonChuan)element).PHTNT = elementBeforeEdit.PHTNT;
            ((GiaVonChuan)element).PHTLT = elementBeforeEdit.PHTLT;
            ((GiaVonChuan)element).THOATHUAN = elementBeforeEdit.THOATHUAN;
            ((GiaVonChuan)element).TTB = elementBeforeEdit.TTB;
            ((GiaVonChuan)element).TTC = elementBeforeEdit.TTC;
            ((GiaVonChuan)element).TTV = elementBeforeEdit.TTV;
            ((GiaVonChuan)element).ECT = elementBeforeEdit.ECT;
            ((GiaVonChuan)element).QUOCTE = elementBeforeEdit.QUOCTE;
            ((GiaVonChuan)element).TUNGAY = elementBeforeEdit.TUNGAY;
            ((GiaVonChuan)element).DENNGAY = elementBeforeEdit.DENNGAY;
            AddEditionEvent($"RowEditCancel event: Editing of Element {((GiaVonChuan)element).ACCOUNTEMAIL} canceled");
        }
        private async Task InvokeModalDelete()
        {
            var request = new List<SingleUpdateRequest>();
            foreach (var item in selectedItems)
            {
                request.Add(new SingleUpdateRequest
                {
                    TABLENAME = "GIAVONCHUAN",
                    IDCOLUMNNAME = "NOITINH",
                    IDCOLUMNVALUE = item.NOITINH
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
