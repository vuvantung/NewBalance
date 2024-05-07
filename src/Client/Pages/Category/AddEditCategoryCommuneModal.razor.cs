using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;

using MudBlazor;

using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using NewBalance.Client.Pages.Catalog;
using NewBalance.Domain.Entities.Doi_Soat.Category;

using System.Threading.Tasks;


namespace NewBalance.Client.Pages.Category
{
    public partial class AddEditCategoryCommuneModal
    {
        [Parameter] public Commune AddEditCommuneModel { get; set; } = new();
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
            var response = await _categoryManager.AddCommuneAsync(AddEditCommuneModel);
            if ( response.code == "SUCCESS" )
            {
                _snackBar.Add("Thêm phường xã thành công", Severity.Success);
                MudDialog.Close(true);
            }
            else
            {
                _snackBar.Add(response.message, Severity.Error);
            }
        }
    }
}
