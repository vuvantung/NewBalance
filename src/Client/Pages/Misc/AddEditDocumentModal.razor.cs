using System;
using System.Collections.Generic;
using NewBalance.Application.Features.Documents.Commands.AddEdit;
using NewBalance.Application.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using NewBalance.Application.Features.DocumentTypes.Queries.GetAll;
using NewBalance.Client.Infrastructure.Managers.Misc.Document;
using NewBalance.Client.Infrastructure.Managers.Misc.DocumentType;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Hosting;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.IServices;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Net.Mime;
using System.Net.Http.Headers;

namespace NewBalance.Client.Pages.Misc
{
    public partial class AddEditDocumentModal
    {
        [Inject] private IDocumentManager DocumentManager { get; set; }
        [Inject] private IDocumentTypeManager DocumentTypeManager { get; set; }
        [Inject] private IDS_MATINH_FILESService _ods_matinh_file_service { get; set; }


        //[Inject] protected IHostingEnvironment hostEnvironment { get; set; }
        //private IHostingEnvironment hostEnvironment;

        [Parameter] public AddEditDocumentCommand AddEditDocumentModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
       


        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<GetAllDocumentTypesResponse> _documentTypes = new();

        //public AddEditDocumentModal()
        //{
           
        //}
        public void Cancel()
        {
            MudDialog.Cancel();
        }

        //[RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        //[RequestSizeLimit(2028 * 1024 * 1024)]
        private async Task SaveAsync()
        {
            // xu ly luu file dữ liệu lớn
            if(AddEditDocumentModel.DocumentTypeId < 1)
            {
                _snackBar.Add("Bạn hãy chọn file cần Upload", Severity.Success);
                MudDialog.Close();
                return;
            }
            var data = AddEditDocumentModel;
            var model = await DocumentTypeManager.GetById(data.DocumentTypeId);

            if (model.Data.Name == "doi_soat_cast_file")
            {
                // Allow reading any file size.
                using var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(_file.OpenReadStream(Int64.MaxValue));

                fileContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Octet);

                content.Add(fileContent, "\"file\"", _file.Name);
                var res_file = await _ods_matinh_file_service.UploadFile(content);

                data.URL = _file.Name;
                data.UploadRequest = null;
            }

            var response = await DocumentManager.SaveAsync(data);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await LoadDocumentTypesAsync();
        }

        private async Task LoadDocumentTypesAsync()
        {
            var data = await DocumentTypeManager.GetAllAsync();
            if (data.Succeeded)
            {
                _documentTypes = data.Data;
            }
        }

        private IBrowserFile _file;

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                //var buffer = new byte[_file.Size];
                //var extension = Path.GetExtension(_file.Name);
                //var format = "application/octet-stream";
                //await _file.OpenReadStream(_file.Size).ReadAsync(buffer);
                //AddEditDocumentModel.URL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                //AddEditDocumentModel.UploadRequest = new UploadRequest { FileName = _file.Name, Data = buffer, UploadType = Application.Enums.UploadType.Document, Extension = extension };
                var buffer = new byte[_file.Size];
                var extension = Path.GetExtension(_file.Name);
                var format = "application/octet-stream";
                await _file.OpenReadStream(long.MaxValue).ReadAsync(buffer);
                AddEditDocumentModel.URL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                AddEditDocumentModel.UploadRequest = new UploadRequest { FileName = _file.Name, Data = buffer, UploadType = Application.Enums.UploadType.Document, Extension = extension };
            }
        }

        private async Task<IEnumerable<int>> SearchDocumentTypes(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _documentTypes.Select(x => x.Id);

            return _documentTypes.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }
    }
}