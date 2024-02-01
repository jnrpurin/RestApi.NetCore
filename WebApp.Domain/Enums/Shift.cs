using System.Text.Json.Serialization;

namespace WebApp.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Shift
    {
        Morning,
        Afternoon,
        Night
    }
}
