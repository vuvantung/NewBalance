using Microsoft.AspNetCore.Components;
using NewBalance.Client.Extensions;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter;
using NewBalance.Domain.Entities.Doi_Soat.Filter;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewBalance.Client.Pages.Category
{
    public partial class CategoryBase
    {
        
        private IEnumerable<FilterData> _accountFilter;
        private string CurrentUserId { get; set; }
        private string _account;
        private string _categoryType;
        private bool loadCategory = false;
        private ClaimsPrincipal _currentUser;
        private bool _loaded;
        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            

            _loaded = true;

            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if ( user == null ) return;
            if ( user.Identity?.IsAuthenticated == true )
            {
                CurrentUserId = user.GetUserId();
            }
        }

        private void HandleLoadCategory()
        {
           loadCategory = true;
        }
    }

}
