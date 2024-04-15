using ClosedXML.Excel;
using ClosedXML.Report;
using NewBalance.Domain.Entities.Doi_Soat.Report;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewBalance.Client.XLS
{
    public class UseTemplateXLS
    {
        public byte[] Edition( Stream streamTemplate, BDT_TH01[] data )
        {
            var template = new XLTemplate(streamTemplate);

            template.AddVariable("BDT_TH01", data);
            template.Generate();

            MemoryStream XLSStream = new();
            template.SaveAs(XLSStream);


            return XLSStream.ToArray();
        }

        public async Task<byte[]> FillIn( HttpClient client, Stream streamTemplate, BDT_TH01[] data, string existingXLS )
        {
            // Open the Templace
            var template = new XLTemplate(streamTemplate);
            // Send Data
            template.AddVariable("BDT_TH01", data);

            template.Generate();

            var sheetCopied = template.Workbook.Worksheet(1);

            var mdFile = await client.GetByteArrayAsync(existingXLS);
            Stream stream = new MemoryStream(mdFile);

            XLWorkbook wb = new XLWorkbook(stream);
            wb.AddWorksheet(sheetCopied);


            MemoryStream XLSStream = new();
            wb.SaveAs(XLSStream);


            return XLSStream.ToArray();
        }
    }
}
