using System.Text.Json;
using NewBalance.Application.Interfaces.Serialization.Options;

namespace NewBalance.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}