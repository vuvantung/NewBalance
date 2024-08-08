using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;

using MudBlazor;

using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Category;
using NewBalance.Client.Infrastructure.Managers.Doi_Soat.Filter;
using NewBalance.Client.Infrastructure.Ultities;
using NewBalance.Client.Pages.Catalog;
using NewBalance.Domain.Entities.Doi_Soat.Category;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace NewBalance.Client.Pages.Category.PostalDirectory
{
    public partial class AddEditCategoryCommuneModal
    {
        [Inject] private IFilterManager _filterManager { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public Commune AddEditCommuneModel { get; set; } = new();
        [Parameter] public string ProvinceCode { get; set; } = "0";
        [Parameter] public string[] provinceList { get; set; }
        [Parameter] public string DistrictCode { get; set; }
        private string[] districtList { get; set; }
        [Inject] private ICategoryManager _categoryManager { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        protected override async Task OnParametersSetAsync()
        {
            districtList = (await _filterManager.GetDistrictFilterAsync(Hepler.GetValueFromTextSelect(ProvinceCode))).Select(x => x.TEXT.ToString()).ToArray();
        }

        private async Task ChangeProvince( string str )
        {
            districtList = (await _filterManager.GetDistrictFilterAsync(Hepler.GetValueFromTextSelect(str))).Select(x => x.TEXT.ToString()).ToArray();
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }
        private async Task SaveAsync()
        {
            AddEditCommuneModel.DISTRICTCODE = Hepler.GetValueFromTextSelect(DistrictCode);
            var response = await _categoryManager.AddCommuneAsync(AddEditCommuneModel);
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
