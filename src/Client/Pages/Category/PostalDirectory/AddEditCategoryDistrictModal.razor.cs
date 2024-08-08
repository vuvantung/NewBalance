using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using NewBalance.Client.Infrastructure.Ultities;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using System;
using System.Threading.Tasks;


namespace NewBalance.Client.Pages.Category.PostalDirectory
{
    public partial class AddEditCategoryDistrictModal
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public District AddEditDistrictModel { get; set; } = new();
        [Parameter] public string ProvinceCode { get; set; }
        [Parameter] public string[] provinceList { get; set; }
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
            AddEditDistrictModel.PROVINCECODE = Convert.ToInt32(Hepler.GetValueFromTextSelect(ProvinceCode));
            var response = await _categoryManager.AddDistrictAsync(AddEditDistrictModel);
            if ( response.code == "SUCCESS" )
            {
                _snackBar.Add((Title == "Tạo mới") ? "Thêm mới thành công" : "Cập nhật thành công", Severity.Success);
                MudDialog.Close(true);
            }
            else
            {
                _snackBar.Add(response.message, Severity.Error);
            }
        }
    }
}
