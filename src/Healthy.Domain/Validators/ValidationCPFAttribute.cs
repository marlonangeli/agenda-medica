using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Healthy.Domain.Validators;

public class ValidationCPFAttribute : ValidationAttribute, IClientValidatable
{
    /// <summary>
    /// Do the validation of the CPF on server side
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool IsValid(object? value)
    {
        if (value is null || string.IsNullOrEmpty(value.ToString()))
            return false;
        
        bool isValid = Validation.ValidateCPF(value.ToString());
        return isValid;
    }

    /// <summary>
    /// Do the validation of the CPF on client side
    /// </summary>
    /// <param name="metadata"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    {
        yield return new ModelClientValidationRule
        {
            ErrorMessage = "CPF inválido",
            ValidationType = "validationcpf"
        };
    }
}