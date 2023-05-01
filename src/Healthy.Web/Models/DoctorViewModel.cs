using System.ComponentModel.DataAnnotations;
using Healthy.Domain.Entities;

namespace Healthy.Web.Models;

public class DoctorViewModel : Doctor
{
    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Especialidades")]
    public List<int> SpecialitiesId { get; set; } = new();
}