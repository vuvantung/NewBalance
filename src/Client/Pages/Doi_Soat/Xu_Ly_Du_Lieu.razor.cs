using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.IServices;
using NewBalance.Client.Extensions;
using NewBalance.Domain.Entities.Misc;
using NewBalance.Shared.Constants.Permission;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewBalance.Client.Pages.Doi_Soat
{
    public partial class Xu_Ly_Du_Lieu
    {
        //[Inject] private IDocumentManager DocumentManager { get; set; }

        [Inject] private IDS_MATINH_FILESService _ods_matinh_file_service { get; set; }

        private IEnumerable<GetAllDS_MATINH_FILESResponse> _pagedData;
        //private IEnumerable<GetAllDS_MATINH_FILESResponse> _pagedData_new;
        private MudTable<GetAllDS_MATINH_FILESResponse> _table;
        private HashSet<GetAllDS_MATINH_FILESResponse> selectedItems = new HashSet<GetAllDS_MATINH_FILESResponse>();
        private HashSet<GetAllDS_MATINH_FILESResponse> selectedItems1 = new HashSet<GetAllDS_MATINH_FILESResponse>();
        private string CurrentUserId { get; set; }
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = true;
        private bool isCheckAll = false;

        private ClaimsPrincipal _currentUser;
        private bool _canSearchDocuments;
        private bool _loaded;
        private bool _canDowloadXu_ly_Du_Lieu;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();

            _canSearchDocuments = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DS_Xu_Ly_Du_Lieu.Search)).Succeeded;
            _canDowloadXu_ly_Du_Lieu = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DS_Xu_Ly_Du_Lieu.Dowload)).Succeeded;
            //_canViewDocumentExtendedAttributes = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DocumentExtendedAttributes.View)).Succeeded;

            _loaded = true;

            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if ( user == null ) return;
            if ( user.Identity?.IsAuthenticated == true )
            {
                CurrentUserId = user.GetUserId();
            }
        }
        private Task HandleCheck( bool value )
        {
            // Set the value in the model
            // Do what you want
            var kq = Task.CompletedTask;
            return kq;
            //return Task.CompletedTask;
        }
        private async Task<TableData<GetAllDS_MATINH_FILESResponse>> ServerReload( TableState state )
        {
            if ( !string.IsNullOrWhiteSpace(_searchString) )
            {
                state.Page = 0;
            }

            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetAllDS_MATINH_FILESResponse> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData( int pageNumber, int pageSize, TableState state )
        {
            ///Dữ liệu oracle
            //pageNumber = pageNumber + 1;
            //pageSize = 1;
            var res_data = await _ods_matinh_file_service.GetDS_MATINH_FILES(pageNumber.ToString(), pageSize.ToString(), 0, 0, 0);
            //var json = JsonConvert.SerializeObject(data_test.data);
            //var data_test_list = JsonConvert.DeserializeObject<List<DS_MATINH_FILES>>(json);
            var data_list = res_data.data;


            _totalItems = res_data.total;// response.TotalCount;
            _currentPage = pageNumber;// response.CurrentPage;



            _pagedData = data_list;

            foreach ( var item in data_list )
            {
                if ( item.STATUS == 1 )
                {
                    selectedItems.Add(item);
                }
            }

        }

        private void OnSearch( string text )
        {
            _searchString = text;
            _table.ReloadServerData();
        }


        private bool IsSelectAllChecked
        {
            get
            {
                var currentPage = _table.CurrentPage;
                var rowsPerPage = _table.RowsPerPage;

                var currentPageItems = _table.FilteredItems.Skip(currentPage * rowsPerPage).Take(rowsPerPage);

                if ( !selectedItems1.Any(x => currentPageItems.Any(y => x == y)) )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        private void Select()
        {
            var currentPage = _table.CurrentPage;
            var rowsPerPage = _table.RowsPerPage;

            var currentPageItems = _table.FilteredItems.Skip(currentPage * rowsPerPage).Take(rowsPerPage);

            if ( !selectedItems1.Any(x => currentPageItems.Any(y => x == y)) )
            {
                foreach ( var item in currentPageItems )
                {
                    selectedItems1.Add(item);
                }
            }
            else
            {
                foreach ( var item in currentPageItems )
                {
                    selectedItems1.Remove(item);
                }
            }
        }

        private async Task SaveAsync()
        {
            List<int> _list = new List<int>();
            string _fullname = _currentUser.GetFirstName() + " " + _currentUser.GetLastName();
            foreach ( var item in selectedItems )
            {
                _list.Add(item.ID);
            }
            var response = await _ods_matinh_file_service.DS_MATINH_FILES_MODIFY_STATUS(_list, _fullname);
            if ( response.code == "success" )
            {
                _snackBar.Add(response.message, Severity.Success);

                await _table.ReloadServerData();
            }
            else
            {
                _snackBar.Add(response.message, Severity.Error);
            }

        }



        private void ManageExtendedAttributes( int documentId )
        {
            _navigationManager.NavigateTo($"/extended-attributes/{typeof(Document).Name}/{documentId}");
        }
    }
}