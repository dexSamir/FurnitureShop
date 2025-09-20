using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FurnitureShop.BL.Utilities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ESortDirection
{
    [Display(Name = "Artan")]
    ASC,
    
    [Display(Name = "Azalan")]
    DESC
}