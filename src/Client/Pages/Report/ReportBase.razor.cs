using Microsoft.AspNetCore.Components;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.GetAll;
using NewBalance.Client.Extensions;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewBalance.Client.Pages.Report
{
    public partial class ReportBase
    {
        [Inject] private IFilterManager _filterManager { get; set; }
        private IEnumerable<FilterData> _accountFilter;
        private IEnumerable<FilterData> _typeReportFilter;
        private string CurrentUserId { get; set; }
        private ClaimsPrincipal _currentUser;
        private bool _loaded;
        private DateTime? _fromDate = DateTime.Today;
        private DateTime? _toDate = DateTime.Today;
        private string _account;
        private string _reportType;
        private bool loadReport { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _accountFilter = await _filterManager.GetAccountFilterAsync();
            _typeReportFilter = await _filterManager.GetTypeReportFilterAsync();
            _loaded = true;

            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if ( user == null ) return;
            if ( user.Identity?.IsAuthenticated == true )
            {
                CurrentUserId = user.GetUserId();
            }
        }

        private void HandleIsLoadChanged( bool value )
        {
            loadReport = value; // Cập nhật giá trị của biến isLoad từ component ra trang
            StateHasChanged(); // Cập nhật UI
        }

        private void HandleChangeReport()
        {
            loadReport = true;
        }
    }
}
