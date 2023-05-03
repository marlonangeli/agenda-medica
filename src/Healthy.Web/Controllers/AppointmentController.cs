using System.Data;
using Healthy.Domain.Entities;
using Healthy.Domain.Enums;
using Healthy.Domain.Interfaces;
using Healthy.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Web.Controllers;

public class AppointmentController : Controller
{
    private readonly IBaseRepository<Appointment> _appointmentRepository;
    private readonly IBaseRepository<Patient> _patientRepository;
    private readonly IBaseRepository<Doctor> _doctorRepository;

    public AppointmentController(IBaseRepository<Appointment> appointmentRepository,
        IBaseRepository<Patient> patientRepository, IBaseRepository<Doctor> doctorRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<IActionResult> Details(int id, string? modelError = null)
    {
        var appointment = await _appointmentRepository.GetQueryable()
            .Include(i => i.Patient)
            .Include(i => i.Doctor)
            .ThenInclude(d => d.Specialities)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (modelError is not null)
        {
            ModelState.AddModelError(string.Empty, modelError);
        }

        return View(appointment);
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var (appointments, total) =
            await _appointmentRepository.GetAllAsync(page, pageSize: ViewConstants.PageSize, includeAll: true);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)total / ViewConstants.PageSize);

        return View(appointments);
    }

    public async Task<IActionResult> Create()
    {
        PopulateDropDownLists();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        try
        {
            RemoveModelStateProperties();

            if (ModelState.IsValid)
            {
                var entity = await _appointmentRepository.AddAsync(appointment);
                return RedirectToAction(nameof(Details), new { id = entity.Id });
            }
        }
        catch (Exception e)
        {
            ModelState?.AddModelError(string.Empty, e.Message);
        }
        finally
        {
            PopulateDropDownLists();
        }

        return View(appointment);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, true);

        var (patients, _) = await _patientRepository.GetAllAsync(1);
        var (doctors, _) = await _doctorRepository.GetAllAsync(1);

        var selectListPatients = new SelectList(patients, nameof(Patient.Id), nameof(Patient.FullName));
        var selectListDoctors = new SelectList(doctors, nameof(Doctor.Id), nameof(Doctor.FullName));

        ViewBag.Patients = selectListPatients;
        ViewBag.Doctors = selectListDoctors;

        return View(appointment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Appointment appointment)
    {
        try
        {
            RemoveModelStateProperties();

            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _appointmentRepository.UpdateAsync(appointment);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ModelState?.AddModelError(string.Empty, e.Message);
        }
        finally
        {
            PopulateDropDownLists();
        }

        return View(appointment);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _appointmentRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Details), new { id = id, modelError = e.Message });
        }
    }

    private async void PopulateDropDownLists()
    {
        var (patients, _) = await _patientRepository.GetAllAsync(1);
        var (doctors, _) = await _doctorRepository.GetAllAsync(1);

        var selectListPatients = new SelectList(patients, nameof(Patient.Id), nameof(Patient.FullName));
        var selectListDoctors = new SelectList(doctors, nameof(Doctor.Id), nameof(Doctor.FullName));

        ViewBag.Patients = selectListPatients;
        ViewBag.Doctors = selectListDoctors;
    }

    private void RemoveModelStateProperties()
    {
        ModelState.Remove(nameof(Appointment.Patient));
        ModelState.Remove(nameof(Appointment.Doctor));
    }

    public async Task<JsonResult> GetCalendarData()
    {
        var query = _appointmentRepository
            .GetQueryable()
            .AsNoTracking()
            .Include(i => i.Patient)
            .Include(i => i.Doctor);

        var appointments = await query
            .ToListAsync();
        var events = new List<object>();

        foreach (var appointment in appointments)
        {
            events.Add(
                new
                {
                    title = $"Consulta {appointment.Id} - {appointment.Patient.FirstName}",
                    start = appointment.Date.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = appointment.Date.AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                    url = $"/Appointment/Details/{appointment.Id}",
                    backgroundColor = appointment.Status switch
                    {
                        AppointmentStatus.Canceled => "#f44336",
                        AppointmentStatus.Completed => "#4caf50",
                        AppointmentStatus.Scheduled => "#ff9800",
                        AppointmentStatus.Confirmed => "#2196f3",
                        _ => "#2196f3"
                    }
                }
            );
        }

        return Json(events);
    }
}