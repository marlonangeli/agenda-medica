using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Healthy.Domain.Validators;

public class ValidationCRMAttribute : ValidationAttribute, IClientValidatable
{
    /// <summary>
    /// Do the validation of the CRM on server side
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool IsValid(object? value)
    {
        if (value is null || string.IsNullOrEmpty(value.ToString()))
            return false;
        
        bool isValid = Validation.ValidateCRM(value.ToString());
        return isValid;
    }

    /// <summary>
    /// Do the validation of the CRM on client side
    /// </summary>
    /// <param name="metadata"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    {
        yield return new ModelClientValidationRule
        {
            ErrorMessage = "CRM inválido",
            ValidationType = "validationcrm"
        };
    }
}