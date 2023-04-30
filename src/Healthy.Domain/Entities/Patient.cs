using System.ComponentModel.DataAnnotations;
using Healthy.Domain.Validators;

namespace Healthy.Domain.Entities;

public class Patient : Person
{
    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "CPF")]
    [ValidationCPF(ErrorMessage = "CPF inválido")]
    public string CPF { get; set; }
    
    public virtual ICollection<Appointment> Appointments { get; set; }
    
    public Patient()
    {
        Appointments = new HashSet<Appointment>();
    }
}