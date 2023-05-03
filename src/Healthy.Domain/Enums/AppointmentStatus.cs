using System.ComponentModel.DataAnnotations;

namespace Healthy.Domain.Enums;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var memberInfo = enumType.GetMember(enumValue.ToString());
        var displayAttribute =
            memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault()
            as DisplayAttribute;
        return displayAttribute?.Name ?? enumValue.ToString();
    }
}

public enum AppointmentStatus
{
    [Display(Name = "Agendada")]
    Scheduled = 1,

    [Display(Name = "Confirmada")]
    Confirmed = 2,

    [Display(Name = "Cancelada")]
    Canceled = 3,

    [Display(Name = "Completa")]
    Completed = 4,

    [Display(Name = "Situação inválida")]
    Invalid = 0
}
