using System.ComponentModel.DataAnnotations;

namespace Healthy.Domain.Entities;

public class Speciality
{
    [Display(Name = "Código")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Nome")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres")]
    public string Name { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; }
    
    public Speciality()
    {
        Doctors = new HashSet<Doctor>();
    }
}