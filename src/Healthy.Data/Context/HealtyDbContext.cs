using Healthy.Domain.Entities;
using Healthy.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Data.Context;

public class HealtyDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Speciality> Specialities { get; set; }

    public HealtyDbContext(DbContextOptions<HealtyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relations
        modelBuilder
            .Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany()
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Doctor>()
            .HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Doctor>()
            .HasMany(d => d.Specialities)
            .WithMany(s => s.Doctors)
            .UsingEntity<DoctorSpeciality>(ds =>
                    ds.HasOne(d => d.Speciality)
                        .WithMany()
                        .HasForeignKey(d => d.SpecialityId),
                ds =>
                    ds.HasOne(s => s.Doctor)
                        .WithMany()
                        .HasForeignKey(s => s.DoctorId),
                ds => ds.HasKey(d => new { d.DoctorId, d.SpecialityId })
            );

        modelBuilder
            .Entity<DoctorSpeciality>()
            .HasKey(ds => new { ds.DoctorId, ds.SpecialityId })
            .HasName("PK_DoctorSpeciality");

        modelBuilder
            .Entity<Patient>()
            .HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Unique fields
        modelBuilder
            .Entity<Doctor>()
            .HasIndex(d => d.CRM)
            .IsUnique();

        modelBuilder
            .Entity<Patient>()
            .HasIndex(p => p.CPF)
            .IsUnique();

        // Seed
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            {
                Id = 1,
                FirstName = "Gabriel",
                LastName = "Trevisan",
                CRM = "PR792268",
                BirthDate = new DateTime(2003, 1, 8),
                Email = "gabrieldamke@alunos.utfpr.edu.br",
                Phone = "(45) 99134-5657",
            },
            new Doctor
            {
                Id = 2,
                FirstName = "Marlon",
                LastName = "Angeli",
                CRM = "PR25472",
                BirthDate = new DateTime(2002, 4, 10),
                Email = "marlonangeli@alunos.utfpr.edu.br",
                Phone = "(45) 98421-4127",
            },
            new Doctor
            {
                Id = 3,
                FirstName = "Mateus",
                LastName = "Stamm",
                CRM = "PR165557",
                BirthDate = new DateTime(2000, 1, 1),
                Email = "mateusstamm@alunos.utfpr.edu.br",
                Phone = "(55) 99931-5511",
            }
        );

        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                Id = 1,
                FirstName = "Arthur",
                LastName = "Henrique",
                CPF = "507.303.030-39",
                BirthDate = new DateTime(1973, 1, 30),
                Email = "artzao@healthy.com",
                Phone = "(11) 99123-4567"
            },
            new Patient
            {
                Id = 2,
                FirstName = "Gabriel",
                LastName = "Stabile",
                CPF = "561.317.130-06",
                BirthDate = new DateTime(1983, 5, 12),
                Email = "gabs@healthy.com",
                Phone = "(45) 99876-5432"
            },
            new Patient
            {
                Id = 3,
                FirstName = "Cheng",
                LastName = "Wei Chih",
                CPF = "771.708.640-96",
                BirthDate = new DateTime(1990, 12, 25),
                Email = "cheng@healthy.com",
                Phone = "(45) 99123-4567"
            },
            new Patient
            {
                Id = 4,
                FirstName = "Guilherme",
                LastName = "Augusto",
                CPF = "372.469.560-85",
                BirthDate = new DateTime(1999, 7, 1),
                Email = "guilherme@healthy.com",
                Phone = "(45) 99876-5432"
            }
        );

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment
            {
                Id = 1,
                DoctorId = 1,
                PatientId = 1,
                Description = "Consulta de rotina",
                Date = DateTime.Now.AddDays(1),
                Invoice = 54.2m,
                Status = AppointmentStatus.Scheduled,
                CreatedAt = DateTime.Now
            },
            new Appointment
            {
                Id = 2,
                DoctorId = 1,
                PatientId = 2,
                Description = "Consulta de rotina",
                Date = DateTime.Now.AddDays(2),
                Invoice = 54.2m,
                Status = AppointmentStatus.Scheduled,
                CreatedAt = DateTime.Now
            },
            new Appointment
            {
                Id = 3,
                DoctorId = 2,
                PatientId = 3,
                Description = "Checagem de exames",
                Date = DateTime.Now.Subtract(TimeSpan.FromDays(2)),
                Invoice = 54.2m,
                Status = AppointmentStatus.Canceled,
                CreatedAt = DateTime.Now.Subtract(TimeSpan.FromDays(3))
            },
            new Appointment
            {
                Id = 4,
                DoctorId = 3,
                PatientId = 4,
                Description = "Operação",
                Date = DateTime.Now,
                Invoice = 54.2m,
                Status = AppointmentStatus.Completed,
                CreatedAt = DateTime.Now
            }
        );

        modelBuilder.Entity<Speciality>().HasData(
            new Speciality
            {
                Id = 1,
                Name = "Cardiologia"
            },
            new Speciality
            {
                Id = 2,
                Name = "Dermatologia"
            },
            new Speciality
            {
                Id = 3,
                Name = "Clínico Geral"
            }
        );

        modelBuilder.Entity<DoctorSpeciality>().HasData(
            new DoctorSpeciality
            {
                SpecialityId = 1,
                DoctorId = 1
            },
            new DoctorSpeciality
            {
                SpecialityId = 3,
                DoctorId = 1
            },
            new DoctorSpeciality
            {
                SpecialityId = 2,
                DoctorId = 1
            },
            new DoctorSpeciality
            {
                SpecialityId = 1,
                DoctorId = 2
            },
            new DoctorSpeciality
            {
                SpecialityId = 2,
                DoctorId = 2
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}