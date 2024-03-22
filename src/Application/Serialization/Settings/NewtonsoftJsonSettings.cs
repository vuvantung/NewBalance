
using NewBalance.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace NewBalance.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}