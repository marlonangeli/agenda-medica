using System.ComponentModel.DataAnnotations;

namespace Healthy.Domain.Enums;

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