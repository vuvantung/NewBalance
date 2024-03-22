using System.Linq;
using NewBalance.Shared.Constants.Localization;
using NewBalance.Shared.Settings;

namespace NewBalance.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";
        //public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "vi-VN";

        //TODO - add server preferences
    }
}