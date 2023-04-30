using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Healthy.Web.Helpers;

public static class EnumHelper
{
    public static IEnumerable<SelectListItem> GetSelectList(Type enumType)
    {
        var values = Enum.GetValues(enumType)
            .Cast<Enum>()
            .Select(
                e =>
                    new SelectListItem
                    {
                        Text = e.GetDisplayName(),
                        Value = Convert.ToInt32(e).ToString()
                    }
            )
            .ToList();

        return values;
    }

    private static string GetDisplayName(this Enum value)
    {
        var displayAttribute =
            value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

        return displayAttribute?.GetName() ?? value.ToString();
    }
}