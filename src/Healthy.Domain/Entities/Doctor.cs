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
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres")]
    public virtual ICollection<Speciality> Specialties { get; set; }
    
    public virtual ICollection<Appointment> Appointments { get; set; }
    
    public Doctor()
    {
        Appointments = new HashSet<Appointment>();
    }
}