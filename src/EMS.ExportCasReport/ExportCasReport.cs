using EMS.ExportCasReport.Helper;
using NewBalance.Domain.Entities.Doi_Soat.ExportCasReport;
using NewBalance.Infrastructure.OR.IRepository;

namespace EMS.ExportCasReport
{
	public class ExportCasReport : BackgroundService
	{
        private readonly ILogger<ExportCasReport> _logger;
        readonly IExportCasReportRepository _exportCasReportRepository;
        readonly int _PAGESIZE = 300000;
        public ExportCasReport( ILogger<ExportCasReport> logger, IExportCasReportRepository exportCasReportRepostiory )
        {
            _logger = logger;
            _exportCasReportRepository = exportCasReportRepostiory;
        }

        protected override async Task ExecuteAsync( CancellationToken stoppingToken )
        {
            while ( !stoppingToken.IsCancellationRequested )
            {
                _logger.LogInformation("Worker start at: {time}", DateTimeOffset.Now);
                try
                {
                    var lsInforFileCasReport = await _exportCasReportRepository.GetListFileCasReportAsync();
                    if ( lsInforFileCasReport is not null )
                    {
                        _logger.LogInformation($"Got {lsInforFileCasReport.Count()} files");
                        foreach ( var inforFile in lsInforFileCasReport )
                        {
                            var outDir = @"C:\Users\Admin\source\repos\NewBalance\src\Server\Files\Doi_Soat\chi_tiet_doanh_thu_gia_von";
                            var fileName = $"Báo cáo {inforFile.TENTINH} từ {inforFile.TUNGAY} đến {inforFile.DENNGAY}.xlsx";
                            var totalPage = (int)Math.Ceiling((double)inforFile.TONGSO / _PAGESIZE);
                            ExcelGenerator excelGenerator = new ExcelGenerator();
                            try
                            {
                                excelGenerator.CreateFileWithTemplateHeaders(outDir, fileName, totalPage);
                            }
                            catch ( Exception ex )
                            {
                                _logger.LogError($"Error {ex}");
                            }

                            var tasks = new List<Task>();
                            List<List<DetailCasReport>> dataLists = new();
                            var index = 1;
                            while(index <= totalPage )
                            {
                                var localIndex = index;
                                var task = Task.Run(async () => {
                                    var data = await _exportCasReportRepository.GetCasReportDetailDataAsync(inforFile.MATINH, inforFile.TUNGAY, inforFile.DENNGAY, localIndex, _PAGESIZE);
                                    lock ( dataLists ) dataLists.Add(data.ToList());
                                });


                                tasks.Add(task);
                                index++;
                                
                            }
                            await Task.WhenAll(tasks);
                            string outputFilePath = Path.Combine(outDir, fileName);
                            try
                            {
                                excelGenerator.FillExcelWithMultipleSheetsCas(dataLists, outputFilePath,$"{inforFile.MATINH}-{inforFile.TENTINH}",$"{inforFile.TUNGAY}-{inforFile.DENNGAY}");
                                var filePath = @"\Files\Doi_Soat\chi_tiet_doanh_thu_gia_von\" + fileName;
                                await _exportCasReportRepository.UpdateFileCasReportSuccessAsync(inforFile.ID, filePath);
                            }
                            catch(Exception ex )
                            {
                                _logger.LogError($"Error {ex}");
                            }
                        }
                    }
                }
                catch ( Exception ex )
                {
                    _logger.LogError($"Error {ex}");
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

