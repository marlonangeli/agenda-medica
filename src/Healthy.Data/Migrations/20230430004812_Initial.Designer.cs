﻿// <auto-generated />
using System;
using Healthy.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Healthy.Data.Migrations
{
    [DbContext(typeof(HealtyDbContext))]
    [Migration("20230430004812_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Healthy.Domain.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("DoctorId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Invoice")
                        .HasColumnType("TEXT");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 4, 29, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7557),
                            Date = new DateTime(2023, 4, 30, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7549),
                            Description = "Consulta de rotina",
                            DoctorId = 1,
                            Invoice = 54.2m,
                            PatientId = 1,
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 4, 29, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7560),
                            Date = new DateTime(2023, 5, 1, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7559),
                            Description = "Consulta de rotina",
                            DoctorId = 1,
                            Invoice = 54.2m,
                            PatientId = 2,
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 4, 26, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7566),
                            Date = new DateTime(2023, 4, 27, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7562),
                            Description = "Checagem de exames",
                            DoctorId = 2,
                            Invoice = 54.2m,
                            PatientId = 3,
                            Status = 3
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 4, 29, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7569),
                            Date = new DateTime(2023, 4, 29, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7568),
                            Description = "Operação",
                            DoctorId = 3,
                            Invoice = 54.2m,
                            PatientId = 4,
                            Status = 4
                        });
                });

            modelBuilder.Entity("Healthy.Domain.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CRM")
                        .IsUnique();

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(2003, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CRM = "PR792268",
                            Email = "gabrieldamke@alunos.utfpr.edu.br",
                            FirstName = "Gabriel",
                            LastName = "Trevisan",
                            Phone = "(45) 99134-5657"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(2002, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CRM = "PR25472",
                            Email = "marlonangeli@alunos.utfpr.edu.br",
                            FirstName = "Marlon",
                            LastName = "Angeli",
                            Phone = "(45) 98421-4127"
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CRM = "PR165557",
                            Email = "mateusstamm@alunos.utfpr.edu.br",
                            FirstName = "Mateus",
                            LastName = "Stamm",
                            Phone = "(55) 99931-5511"
                        });
                });

            modelBuilder.Entity("Healthy.Domain.Entities.DoctorSpeciality", b =>
                {
                    b.Property<int>("DoctorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpecialityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DoctorId", "SpecialityId")
                        .HasName("PK_DoctorSpeciality");

                    b.HasIndex("SpecialityId");

                    b.ToTable("DoctorSpeciality");

                    b.HasData(
                        new
                        {
                            DoctorId = 1,
                            SpecialityId = 1
                        },
                        new
                        {
                            DoctorId = 1,
                            SpecialityId = 3
                        },
                        new
                        {
                            DoctorId = 1,
                            SpecialityId = 2
                        },
                        new
                        {
                            DoctorId = 2,
                            SpecialityId = 1
                        },
                        new
                        {
                            DoctorId = 2,
                            SpecialityId = 2
                        });
                });

            modelBuilder.Entity("Healthy.Domain.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1973, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CPF = "507.303.030-39",
                            Email = "artzao@healthy.com",
                            FirstName = "Arthur",
                            LastName = "Henrique",
                            Phone = "(11) 99123-4567"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(1983, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CPF = "561.317.130-06",
                            Email = "gabs@healthy.com",
                            FirstName = "Gabriel",
                            LastName = "Stabile",
                            Phone = "(45) 99876-5432"
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateTime(1990, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CPF = "771.708.640-96",
                            Email = "cheng@healthy.com",
                            FirstName = "Cheng",
                            LastName = "Wei Chih",
                            Phone = "(45) 99123-4567"
                        },
                        new
                        {
                            Id = 4,
                            BirthDate = new DateTime(1999, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CPF = "372.469.560-85",
                            Email = "guilherme@healthy.com",
                            FirstName = "Guilherme",
                            LastName = "Augusto",
                            Phone = "(45) 99876-5432"
                        });
                });

            modelBuilder.Entity("Healthy.Domain.Entities.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Specialities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cardiologia"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Dermatologia"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Clínico Geral"
                        });
                });

            modelBuilder.Entity("Healthy.Domain.Entities.Appointment", b =>
                {
                    b.HasOne("Healthy.Domain.Entities.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Healthy.Domain.Entities.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Healthy.Domain.Entities.DoctorSpeciality", b =>
                {
                    b.HasOne("Healthy.Domain.Entities.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Healthy.Domain.Entities.Speciality", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("Healthy.Domain.Entities.Doctor", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Healthy.Domain.Entities.Patient", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}