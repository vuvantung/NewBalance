using NewBalance.Shared.Settings;
using System.Threading.Tasks;
using NewBalance.Shared.Wrapper;

namespace NewBalance.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}