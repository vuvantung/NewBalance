using Microsoft.JSInterop;
using NewBalance.Domain.Entities.Doi_Soat.Report;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewBalance.Client.XLS
{
    public class Excel
    {
        

        public async Task TemplateWeatherForecastAsync( IJSRuntime js,
                                                       Stream streamTemplate,
                                                       BDT_TH01[] data,
                                                       string isType,
                                                       string filename = "export.xlsx" )
        {
            var templateXLS = new UseTemplateXLS();
            var XLSStream = templateXLS.Edition(streamTemplate, data, isType);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\tmpcreate\" + filename + @"\");
            using ( var fileStream = new FileStream(filePath, FileMode.Create) )
            {
                await fileStream.WriteAsync(XLSStream, 0, XLSStream.Length);
            }
            await js.InvokeVoidAsync("BlazorDownloadFile", filename, XLSStream);
        }


        public async Task TemplateOnExistingFileAsync( HttpClient client,
                                                      IJSRuntime js,
                                                      Stream streamTemplate,
                                                      BDT_TH01[] data,
                                                      string existingFile )
        {
            var templateXLS = new UseTemplateXLS();
            var XLSStream = await templateXLS.FillIn(client, streamTemplate, data, existingFile);

            await js.InvokeVoidAsync("BlazorDownloadFile", "export.xlsx", XLSStream);
        }
    }

}
