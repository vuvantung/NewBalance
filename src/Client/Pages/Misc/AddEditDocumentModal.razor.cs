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

namespace NewBalance.Client.Pages.Misc
{
    public partial class AddEditDocumentModal
    {
        [Inject] private IDocumentManager DocumentManager { get; set; }
        [Inject] private IDocumentTypeManager DocumentTypeManager { get; set; }

        [Parameter] public AddEditDocumentCommand AddEditDocumentModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<GetAllDocumentTypesResponse> _documentTypes = new();

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        //[RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        [RequestSizeLimit(2028*1024*1024)]
        private async Task SaveAsync()
        {

            //// xử lý save file
            /////var data = AddEditDocumentModel;
            //var request = AddEditDocumentModel.UploadRequest;
            ////var file = model.
            //if (request != null)
            //{

            //    var streamData = new MemoryStream(request.Data);
            //    if (streamData.Length > 0)
            //    {
            //        try
            //        {
            //            string folder = "Doi_Soat";
            //            var folderName = Path.Combine("Files", folder);
            //            //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //            //string FilePath = Path.Combine(Directory.GetCurrentDirectory(), pathToSave);

            //            //if (!Directory.Exists(FilePath))
            //            //    Directory.CreateDirectory(FilePath);




            //            ////var folder = request.UploadType.ToDescriptionString();
            //            //var folderName = FilePath;// Path.Combine("Files", folder);
            //            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //            bool exists = System.IO.Directory.Exists(pathToSave);
            //            if (!exists)
            //                System.IO.Directory.CreateDirectory(pathToSave);
            //            var fileName = request.FileName.Trim('"');
            //            var fullPath = Path.Combine(pathToSave, fileName);
            //            var dbPath = Path.Combine("D:\\EMS\\Project\\ReportSystem\\src\\Client\\Files\\Doi_Soat\\Xu_Ly_Du_Lieu", fileName);
            //            var dbPathtest = "D:\\EMS\\Project\\ReportSystem\\src\\Client\\Files\\Doi_Soat\\Xu_Ly_Du_Lieu\\" + fileName;
            //            //if (File.Exists(dbPath))
            //            //{
            //            //    dbPath = NextAvailableFilename(dbPath);
            //            //    fullPath = NextAvailableFilename(fullPath);
            //            //}
            //            using (var stream = new FileStream(dbPathtest, FileMode.Create))
            //            {
            //                stream.Position = 0;
            //                streamData.CopyTo(stream);
            //            }
            //            data.URL = dbPath;
            //            //data.UploadRequest = null;
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.ToString());
            //        }

            //    }
            //}


            var response = await DocumentManager.SaveAsync(AddEditDocumentModel);
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
                var buffer = new byte[_file.Size];
                var extension = Path.GetExtension(_file.Name);
                var format = "application/octet-stream";
                await _file.OpenReadStream(_file.Size).ReadAsync(buffer);
                AddEditDocumentModel.URL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                AddEditDocumentModel.UploadRequest = new UploadRequest { FileName= _file.Name, Data = buffer, UploadType = Application.Enums.UploadType.Document, Extension = extension };
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