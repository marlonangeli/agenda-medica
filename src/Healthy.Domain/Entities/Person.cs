using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Healthy.Domain.Validators;

namespace Healthy.Domain.Entities;

public class Person : IEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Primeiro nome")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Sobrenome")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres")]
    public string LastName { get; set; }

    [NotMapped]
    [Display(Name = "Nome completo")]
    public string FullName => FirstName + " " + LastName;

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Data de nascimento")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date, ErrorMessage = "Data inválida")]
    [ValidationBirthDate(ErrorMessage =
        "Data de nascimento inválida, deve ser anterior a data atual e posterior a 01/01/1900")]
    public DateTime BirthDate { get; set; }

    [NotMapped] [Display(Name = "Idade")] public int Age => (int)((DateTime.Now - BirthDate).TotalDays / 365.25);

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Telefone")]
    [Phone(ErrorMessage = "Telefone inválido")]
    public string Phone { get; set; }
}