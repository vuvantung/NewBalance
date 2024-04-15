using NewBalance.Application.Extensions;
using NewBalance.Infrastructure.Extensions;
using NewBalance.Server.Extensions;
using NewBalance.Server.Middlewares;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using NewBalance.Server.Filters;
using Microsoft.Extensions.Localization;
using Asp.Versioning;
using NewBalance.Infrastructure.OR.IRepository;
using NewBalance.Infrastructure.OR.Repository;
using NewBalance.Server.Settings.Managers.Preferences;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.IServices;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http.Features;
using System.Net.Http;
using System;

namespace NewBalance.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
           

            services.AddCors();
            services.AddSignalR();
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            services.AddCurrentUserService();
            services.AddSerialization();
            services.AddDatabase(_configuration);
            services.AddServerStorage(); //TODO - should implement ServerStorageProvider to work correctly!
            services.AddScoped<ServerPreferenceManager>();
            #region OR

            services.AddScoped<IDS_MATINH_FILESRepository, DS_MATINH_FILESRepository>();
            services.AddScoped<IFilterRepository, FilterRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            
            #endregion

            services.AddServerLocalization();
            services.AddIdentity();
            services.AddJwtAuthentication(services.GetApplicationSettings(_configuration));
            services.AddApplicationLayer();
            services.AddApplicationServices();
            services.AddRepositories();
            services.AddExtendedAttributesUnitOfWork();
            services.AddSharedInfrastructure(_configuration);
            services.RegisterSwagger();
            services.AddInfrastructureMappings();
            services.AddHangfire(x => x.UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();
            services.AddControllers().AddValidators();
            services.AddExtendedAttributesValidators();
            services.AddExtendedAttributesHandlers();
            services.AddRazorPages();
            //services.AddServerSideBlazor().AddHubOptions(o =>
            //{
            //    o.MaximumReceiveMessageSize = 100 * 1024 * 1024;
            //});
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 200*1024*1024; // 200 MB
            });

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddLazyCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStringLocalizer<Startup> localizer)
        {
            app.UseCors();
            app.UseExceptionHandling(env);
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files")
            });
            app.UseRequestLocalizationByCulture();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                DashboardTitle = localizer["BlazorHero Jobs"],
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            app.UseEndpoints();
            app.ConfigureSwagger();
            app.Initialize(_configuration);

        }
    }
}