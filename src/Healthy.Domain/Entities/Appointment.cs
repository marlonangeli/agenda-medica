using System.ComponentModel.DataAnnotations;
using Healthy.Domain.Enums;
using Healthy.Domain.Validators;

namespace Healthy.Domain.Entities;

public class Appointment
{
    [Display(Name = "Código")] public int Id { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Data e hora da consulta")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime, ErrorMessage = "Data inválida")]
    [ValidationAppointmentDate(ErrorMessage =
        "Data de consulta inválida, deve ser posterior a data atual limitado a 1 ano")]
    public DateTime Date { get; set; }

    [Display(Name = "Criado em")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime, ErrorMessage = "Data inválida")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Médico")]
    public int DoctorId { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Paciente")]
    public int PatientId { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Descrição")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [Display(Name = "Situação")]
    public AppointmentStatus Status { get; set; }

    [Display(Name = "Médico")] public Doctor Doctor { get; set; }

    [Display(Name = "Paciente")] public Patient Patient { get; set; }

    [Display(Name = "Valor da consulta")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal Invoice { get; set; }
}