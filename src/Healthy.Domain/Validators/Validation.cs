namespace Healthy.Domain.Validators;

internal static class Validation
{
    /// <summary>
    /// Validate CPF (Cadastro de Pessoa Física)
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns></returns>
    public static bool ValidateCPF(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
        {
            return false;
        }

        cpf = cpf.RemoveMask();

        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
        {
            return false;
        }

        if (cpf.Distinct().Count() == 1)
        {
            return false;
        }

        int[] weights1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] weights2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int[] digits = cpf.Select(c => c - '0').ToArray();

        int sum1 = digits.Take(9).Zip(weights1, (a, b) => a * b).Sum();
        int sum2 = digits.Take(10).Zip(weights2, (a, b) => a * b).Sum();

        int remainder1 = sum1 % 11;
        int remainder2 = sum2 % 11;
        
        if (remainder1 < 2)
            remainder1 = 0;
        else
            remainder1 = 11 - remainder1;
        
        if (remainder2 < 2)
            remainder2 = 0;
        else
            remainder2 = 11 - remainder2;

        return digits[9] == remainder1 && digits[10] == remainder2;
    }

    /// <summary>
    /// Validate CRM (Cadastro de Registro Médico)
    /// </summary>
    /// <param name="crm"></param>
    /// <returns></returns>
    public static bool ValidateCRM(string? crm)
    {
        if (string.IsNullOrEmpty(crm))
            return false;

        return crm.All(char.IsDigit);
    }
    
    /// <summary>
    /// Validate birth date, can't register a person before 1900 or after current date
    /// </summary>
    /// <param name="birthDate"></param>
    /// <returns></returns>
    public static bool ValidadeBirthDate(DateTime birthDate)
    {
        DateTime minDate = DateTime.Now.AddYears(-120);
        DateTime maxDate = DateTime.Now;
        
        if (birthDate < minDate || birthDate > maxDate)
            return false;
        
        return true;
    }
    
    
    /// <summary>
    /// Validate appointment date, can't register an appointment before current date or after 1 year
    /// </summary>
    /// <param name="appointmentDate"></param>
    /// <returns></returns>
    public static bool ValidateAppointmentDate(DateTime appointmentDate)
    {
        DateTime minDate = DateTime.Now;
        DateTime maxDate = DateTime.Now.AddYears(1);
        
        if (appointmentDate < minDate || appointmentDate > maxDate)
            return false;
        
        return true;
    }
    
    private static string RemoveMask(this string cpf) => new(cpf.Where(char.IsDigit).ToArray());
}