using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Infrastructure.Extensions
{
    public static class ExcelExport
    {
        public static Stream UpdateDataIntoExcelTemplate<T>( List<T> cList, FileInfo path, string startCell )
        {
            Stream stream = new MemoryStream();
            if ( path.Exists )
            {
                using ( ExcelPackage p = new ExcelPackage(path) )
                {
                    ExcelWorksheet wsEstimate = p.Workbook.Worksheets["Sheet1"];

                    // Parse the start cell to get row and column indices
                    int startRow = int.Parse(startCell.Substring(1));
                    char startColumn = startCell[0];

                    // Determine the end range dynamically
                    int endRow = startRow + cList.Count - 1; // Calculate end row from start cell and count of items

                    int endColumn = typeof(T).GetProperties().Length; // Counting the number of properties in the class

                    // Construct the range string
                    string range = $"{startColumn}{startRow}:{ExcelColumnFromNumber(endColumn)}{endRow}";

                    // Load data into the specified range
                    wsEstimate.Cells[range].LoadFromCollection(cList);

                    // Save the workbook to the stream
                    p.SaveAs(stream);
                    stream.Position = 0;
                }
            }
            return stream;
        }
        private static string ExcelColumnFromNumber( int column )
        {
            int dividend = column;
            string columnName = "";

            while ( dividend > 0 )
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
    }
}
