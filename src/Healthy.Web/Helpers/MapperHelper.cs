using Healthy.Domain.Entities;
using Healthy.Web.Models;

namespace Healthy.Web.Helpers;

public static class MapperHelper
{
    public static Doctor Map(this DoctorViewModel? doctor)
    {
        if (doctor == null) return new Doctor();
        return new Doctor
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            BirthDate = doctor.BirthDate,
            Email = doctor.Email,
            Phone = doctor.Phone,
            Specialities = doctor.SpecialitiesId.Select(s => new Speciality { Id = s }).ToList()
        };
    }
    
    public static DoctorViewModel Map(this Doctor? doctor)
    {
        if (doctor is null)
            return new DoctorViewModel();
        return new DoctorViewModel
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            BirthDate = doctor.BirthDate,
            Email = doctor.Email,
            Phone = doctor.Phone,
            SpecialitiesId = doctor.Specialities.Select(s => s.Id).ToList()
        };
    }
}