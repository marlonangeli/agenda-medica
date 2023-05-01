using System.ComponentModel.DataAnnotations;
using Healthy.Domain.Validators;

namespace Healthy.Domain.Entities;

public class Doctor : Person
{
    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "CRM")]
    [ValidationCRM(ErrorMessage = "CRM inválido")]
    public string CRM { get; set; }
    
    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Especialidades")]
    public virtual ICollection<Speciality> Specialities { get; set; }
    
    public virtual ICollection<Appointment> Appointments { get; set; }
    
    public Doctor()
    {
        Appointments = new HashSet<Appointment>();
        Specialities = new HashSet<Speciality>();
    }
}