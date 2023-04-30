using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using static System.DateTime;

namespace Healthy.Domain.Validators;

public class ValidationAppointmentDateAttribute : ValidationAttribute, IClientValidatable
{
    /// <summary>
    /// Do the validation of the appointment date on server side
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool IsValid(object? value)
    {
        switch (value)
        {
            case DateTime time:
                return Validation.ValidateAppointmentDate(time);
            case string str:
                TryParse(str, out var dateTime);
                return Validation.ValidateAppointmentDate(dateTime);
            default:
                return false;
        }
    }

    /// <summary>
    /// Do the validation of the appointment date on client side
    /// </summary>
    /// <param name="metadata"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    {
        yield return new ModelClientValidationRule
        {
            ErrorMessage = "Data de consulta inválida",
            ValidationType = "validationappointmentdate"
        };
    }
}