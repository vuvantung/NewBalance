using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using System.Threading.Tasks;


namespace NewBalance.Client.Pages.Category
{
    public partial class AddEditCategoryProvinceModal
    {
        private Province AddEditProvinceModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }
        private async Task SaveAsync()
        {
            
        }
    }
}
