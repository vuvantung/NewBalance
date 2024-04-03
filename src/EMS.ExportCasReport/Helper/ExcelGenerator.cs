
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NewBalance.Domain.Entities.Doi_Soat.ExportCasReport;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ExportCasReport.Helper
{
    public class ExcelGenerator
    {
        public bool CreateFileWithTemplateHeaders( string outputDirectory, string fileName, int numberOfSheets )
        {
            try
            {
                string outputFilePath = Path.Combine(outputDirectory, fileName);

                string templateFilePath = "Template/Mau_Chi_Tiet_Buu_Gui_Doi_Soat.xlsx";
                if ( File.Exists(outputFilePath) )
                {
                    File.Delete(outputFilePath);
                }
                // Copy the template file
                File.Copy(templateFilePath, outputFilePath, true);

                // Open the new file
                using ( SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(outputFilePath, true) )
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    WorksheetPart templateSheetPart = workbookPart.WorksheetParts.First(); // Assuming your template has only one sheet

                    // Get the XML representation of the template sheet
                    SheetData templateSheetData = templateSheetPart.Worksheet.Elements<SheetData>().First();
                    // Add new sheets based on the template sheet
                    for ( int i = 1; i < numberOfSheets; i++ )
                    {

                        // Create a new worksheet part
                        WorksheetPart newSheetPart = workbookPart.AddNewPart<WorksheetPart>();

                        // Clone the template sheet structure
                        newSheetPart.Worksheet = new Worksheet();
                        SheetData newSheetData = new SheetData();
                        newSheetData.InnerXml = templateSheetData.InnerXml;
                        newSheetPart.Worksheet.Append(newSheetData);

                        // Add the new sheet to the workbook
                        Sheets sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
                        string relationshipId = workbookPart.GetIdOfPart(newSheetPart);
                        uint sheetId = (uint)(sheets.Count() + 1);
                        string sheetName = "Sheet" + sheetId;

                        Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
                        sheets.Append(sheet);
                    }

                    // Save the changes
                    workbookPart.Workbook.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void FillExcelWithMultipleSheetsCas( List<List<DetailCasReport>> allDataLists, string excelFilePath )
        {
            FileInfo file = new FileInfo(excelFilePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ( ExcelPackage package = new ExcelPackage(file) )
            {
                for ( int i = 0; i < allDataLists.Count; i++ )
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[i]; // Sheet có chỉ mục bắt đầu từ 1

                    List<DetailCasReport> dataList = allDataLists[i];

                    // Xác định dòng bắt đầu điền dữ liệu
                    int startRow = 9; // Ví dụ: dòng đầu tiên

                    // Duyệt qua danh sách và điền dữ liệu vào từng dòng
                    for ( int j = 0; j < dataList.Count; j++ )
                    {
                        var index = j + 1;
                        DetailCasReport item = dataList[j];
                        worksheet.Cells[startRow + j, 1].Value = index;
                        worksheet.Cells[startRow + j, 2].Value = item.MAE1;
                        worksheet.Cells[startRow + j, 3].Value = item.NGAYPHATHANH;
                        worksheet.Cells[startRow + j, 4].Value = item.MABCNHAN;
                        worksheet.Cells[startRow + j, 5].Value = item.MABCTRA;
                        worksheet.Cells[startRow + j, 6].Value = item.MAKH;
                        worksheet.Cells[startRow + j, 7].Value = item.BATCHCODE;
                        worksheet.Cells[startRow + j, 8].Value = item.VAN_DON_CHU;
                        worksheet.Cells[startRow + j, 9].Value = item.MADVCHINH;
                        worksheet.Cells[startRow + j, 10].Value = item.MADVCT;
                        worksheet.Cells[startRow + j, 11].Value = item.KHOILUONG;
                        worksheet.Cells[startRow + j, 12].Value = item.KLQD;
                        worksheet.Cells[startRow + j, 13].Value = item.EDI;
                        worksheet.Cells[startRow + j, 14].Value = item.PPVX;
                        worksheet.Cells[startRow + j, 15].Value = item.PPXD;
                        worksheet.Cells[startRow + j, 16].Value = item.PPHK;
                        worksheet.Cells[startRow + j, 17].Value = item.CUOCCS;
                        worksheet.Cells[startRow + j, 18].Value = item.CUOCDVCT;
                        worksheet.Cells[startRow + j, 19].Value = item.TYLEGIAVON;
                        worksheet.Cells[startRow + j, 20].Value = item.GIAVON;
                        worksheet.Cells[startRow + j, 21].Value = item.TYLEGIAVONDVCTDACBIET;
                        worksheet.Cells[startRow + j, 22].Value = item.GIAVONDVCTDACBIET;
                        worksheet.Cells[startRow + j, 23].Value = item.Thu_Lao_Cong_Phat;
                        worksheet.Cells[startRow + j, 24].Value = item.Cuoc_Chuyen_Hoan;
                        worksheet.Cells[startRow + j, 25].Value = item.Hoan_Cuoc;
                        worksheet.Cells[startRow + j, 26].Value = item.Trang_Thai;
                        // Tiếp tục điền các trường dữ liệu khác tương tự
                    }
                }

                // Lưu thay đổi vào tệp Excel
                package.Save();
            }
        }

    }
}
