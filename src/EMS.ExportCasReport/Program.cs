using EMS.ExportCasReport;
using NewBalance.Application.Interfaces.Common;
using NewBalance.Infrastructure.OR.IRepository;
using NewBalance.Infrastructure.OR.Repository;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IExportCasReportRepository, ExportCasRepository>();
builder.Services.AddHostedService<ExportCasReport>();
var host = builder.Build();
host.Run();
