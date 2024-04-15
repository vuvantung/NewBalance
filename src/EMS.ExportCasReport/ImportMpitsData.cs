using EMS.ExportCasReport.Helper;
using NewBalance.Infrastructure.OR.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ExportCasReport
{
    public class ImportMpitsData : BackgroundService
    {
        private readonly ILogger<ImportMpitsData> _logger;
        readonly IExportCasReportRepository _exportCasReportRepository;

        public ImportMpitsData( ILogger<ImportMpitsData> logger, IExportCasReportRepository exportCasReportRepository )
        {
            _logger = logger;
            _exportCasReportRepository = exportCasReportRepository;
        }

        protected async override Task ExecuteAsync( CancellationToken stoppingToken )
        {
            while ( !stoppingToken.IsCancellationRequested )
            {
                _logger.LogInformation("Worker start at: {time}", DateTimeOffset.Now);
                // Đường dẫn đến thư mục cần tìm
                string folderPath = @"C:\Users\Admin\source\repos\NewBalance\src\Server\Files\Doi_Soat\cast_file";
                string folderPathDone = @"C:\Users\Admin\source\repos\NewBalance\src\Server\Files\Doi_Soat\cast_file_done";
                // Kiểm tra xem thư mục có tồn tại không
                if ( Directory.Exists(folderPath) )
                {
                    // Lấy danh sách tất cả các tệp trong thư mục
                    string[] files = Directory.GetFiles(folderPath);
                    if(files.Count() > 0 )
                    {
                        // Duyệt qua từng tệp và kiểm tra phần mở rộng
                        foreach ( string file in files )
                        {
                            // Kiểm tra nếu là tệp Excel (.xlsx hoặc .xls)
                            if ( Path.GetExtension(file).Equals(".xlsx", StringComparison.OrdinalIgnoreCase) ||
                                Path.GetExtension(file).Equals(".xls", StringComparison.OrdinalIgnoreCase) )
                            {
                                ExcelGenerator excelGenerator = new ExcelGenerator();
                                var xml = excelGenerator.ReadExcelToXml(file);
                                var res = await _exportCasReportRepository.ImportXmlCastDataAsync(xml, file);
                                if(res.code == "SUCCESS" )
                                {
                                    string destinationFilePath = Path.Combine(folderPathDone, Path.GetFileName(file));
                                    File.Move(file, destinationFilePath);
                                    _logger.LogInformation($"Import file {file} successfully");
                                }
                                else
                                {
                                    _logger.LogInformation($"Import file {file} failed");
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        _logger.LogInformation($"No file to Import ");
                    }
                }
                else
                {
                    _logger.LogInformation("Folder does not exits");
                }
                _logger.LogInformation("End start at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
