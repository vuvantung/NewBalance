﻿using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using System.Threading.Tasks;


namespace NewBalance.Client.Pages.Category.PostalDirectory
{
    public partial class AddEditCategoryProvinceModal
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public Province AddEditProvinceModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] private ICategoryManager _categoryManager { get; set; }
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }
        private async Task SaveAsync()
        {
            var response = await _categoryManager.AddProvinceAsync(AddEditProvinceModel);
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
