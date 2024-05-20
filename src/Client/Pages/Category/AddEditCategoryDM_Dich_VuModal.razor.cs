using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;

using MudBlazor;

using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using NewBalance.Client.Pages.Catalog;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using System;
using System.Threading.Tasks;


namespace NewBalance.Client.Pages.Category
{
    public partial class AddEditCategoryDM_Dich_VuModal
    {
        [Parameter] public DM_Dich_Vu AddEditDM_Dich_VuModel { get; set; } = new();
        [Inject] private ICategoryManager _categoryManager { get; set; }

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }
        private async Task SaveAsync()
        { 
            var response = await _categoryManager.AddDM_Dich_VuAsync(AddEditDM_Dich_VuModel);
            if (response.code == "SUCCESS")
            {
                _snackBar.Add("Thêm dich vụ thành công", Severity.Success);
                MudDialog.Close(true);
            }
            else
            {
                _snackBar.Add(response.message, Severity.Error);
            }
        }

       
    }
}
