using NewBalance.Client.Extensions;
using NewBalance.Client.Infrastructure.Managers.Preferences;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NewBalance.Client.Infrastructure.Settings;
using NewBalance.Shared.Constants.Localization;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.IServices;
using NewBalance.Application.Features.Doi_Soat.Danh_Muc.Queries.Services;
using NewBalance.Application.Interfaces.Common;
using EMS.Internal.BlazorWeb.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace NewBalance.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder
            .CreateDefault(args)
                          .AddRootComponents()
                          .AddClientServices();
            builder.Services.AddScoped<IDS_MATINH_FILESService, DS_MATINH_FILESService>();
            //builder.Services.AddScoped<HttpClientFactoryWrapper>();
            
            var host = builder.Build();
            
            var storageService = host.Services.GetRequiredService<ClientPreferenceManager>();
            
            if (storageService != null)
            {
                CultureInfo culture;
                var preference = await storageService.GetPreference() as ClientPreference;
                if (preference != null)
                    culture = new CultureInfo(preference.LanguageCode);
                else
                    culture = new CultureInfo(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US");
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
            await builder.Build().RunAsync();
        }
    }
}