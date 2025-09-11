using System.Text.Json.Serialization;

namespace FurnitureShop.BL.Utilities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ESortDirection
{
    ASC,
    DESC
}