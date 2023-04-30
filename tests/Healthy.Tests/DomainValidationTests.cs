using Healthy.Domain.Validators;

namespace Healthy.Tests;

public class DomainTests
{
    private ValidationAppointmentDateAttribute _appointmentDateValidator;
    private ValidationBirthDateAttribute _birthDateValidator;
    private ValidationCPFAttribute _cpfValidator;
    private ValidationCRMAttribute _crmValidator;

    [SetUp]
    public void Setup()
    {
        _appointmentDateValidator = new ValidationAppointmentDateAttribute();
        _birthDateValidator = new ValidationBirthDateAttribute();
        _cpfValidator = new ValidationCPFAttribute();
        _crmValidator = new ValidationCRMAttribute();
    }

    [Test]
    public void AppointmentDate_ShouldBeValid()
    {
        var validDate = DateTime.Now.AddDays(1);
        var result = _appointmentDateValidator.IsValid(validDate);

        Assert.IsTrue(result);
    }

    [Test]
    public void AppointmentDate_ShouldBeInvalid()
    {
        var invalidDate = DateTime.Now.AddYears(2);
        var result = _appointmentDateValidator.IsValid(invalidDate);

        Assert.IsFalse(result);
    }

    [Test]
    public void BirthDate_ShouldBeValid()
    {
        var validDate = DateTime.Now.AddYears(-20);
        var result = _birthDateValidator.IsValid(validDate);

        Assert.IsTrue(result);
    }

    [Test]
    public void BirthDate_ShouldBeInvalid()
    {
        var invalidDate = DateTime.Now.AddYears(-120);
        var result = _birthDateValidator.IsValid(invalidDate);

        Assert.IsFalse(result);
    }

    [Test]
    public void CPF_ShouldBeValid()
    {
        var validCPF = "12345678909";
        var result = _cpfValidator.IsValid(validCPF);

        Assert.IsTrue(result);
    }

    [Test]
    public void CPF_ShouldBeInvalid()
    {
        var invalidCPF = "12345678910";
        var result = _cpfValidator.IsValid(invalidCPF);

        Assert.IsFalse(result);
    }

    [Test]
    public void CRM_ShouldBeValid()
    {
        var validCRM = "1225472";
        var result = _crmValidator.IsValid(validCRM);

        Assert.IsTrue(result);
    }

    [Test]
    public void CRM_ShouldBeInvalid()
    {
        var invalidCRM = "KK123456";
        var result = _crmValidator.IsValid(invalidCRM);

        Assert.IsFalse(result);
    }
}