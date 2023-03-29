using System.Text.Json.Serialization;

namespace RPG_Api.Models.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))] //converting enum array to string
    public enum RPG_Class
    {
        Knight = 1,
        Mage = 2, 
        Cleric = 3,
    }
}
