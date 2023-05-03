using Healthy.Domain.Entities;
using Healthy.Domain.Interfaces;
using Healthy.Web.Constants;
using Healthy.Web.Helpers;
using Healthy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Web.Controllers;

public class DoctorController : Controller
{
    private readonly IBaseRepository<Doctor> _doctorRepository;
    private readonly IBaseRepository<Speciality> _specialtyRepository;

    public DoctorController(IBaseRepository<Doctor> doctorRepository, IBaseRepository<Speciality> specialtyRepository)
    {
        _doctorRepository = doctorRepository;
        _specialtyRepository = specialtyRepository;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var (doctors, total) =
            await _doctorRepository.GetAllAsync(page, pageSize: ViewConstants.PageSize, orderBy: p => p.FirstName);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)total / ViewConstants.PageSize);

        return View(doctors);
    }

    public async Task<IActionResult> Details(int id, string? modelError = null)
    {
        var doctor = await _doctorRepository.GetQueryable()
            .Include(i => i.Appointments)
            .ThenInclude(i => i.Patient)
            .Include(i => i.Specialities)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (modelError is not null)
        {
            ModelState.AddModelError(string.Empty, modelError);
        }

        return View(doctor);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _doctorRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Details), new { id, modelError = e.Message });
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);
        PopulateSpecialties();

        var doctorViewModel = doctor.Map();

        return View(doctorViewModel);
    }

    public IActionResult Create()
    {
        PopulateSpecialties();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DoctorViewModel doctor)
    {
        try
        {
            RemoveModelStateProperties();

            if (ModelState.IsValid)
            {
                var doctorEntity = doctor.Map();
                var specialities = await _specialtyRepository.GetQueryable()
                    .Where(w => doctorEntity.Specialities.Select(s => s.Id).Contains(w.Id))
                    .ToListAsync();
                
                // Database error
                doctorEntity.Specialities = specialities;
                await _doctorRepository.AddAsync(doctorEntity);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ModelState?.AddModelError(string.Empty, e.Message);
        }
        finally
        {
            PopulateSpecialties();
        }

        return View(doctor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DoctorViewModel doctor)
    {
        try
        {
            RemoveModelStateProperties();

            if (ModelState.IsValid)
            {
                var doctorEntity = doctor.Map();
                var specialities = await _specialtyRepository.GetQueryable()
                    .Where(w => doctorEntity.Specialities.Select(s => s.Id).Contains(w.Id))
                    .ToListAsync();
                
                // TODO: Unique constraint error
                // Database error
                doctorEntity.Specialities = specialities;
                await _doctorRepository.UpdateAsync(doctorEntity);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ModelState?.AddModelError(string.Empty, e.Message);
        }
        finally
        {
            PopulateSpecialties();
        }

        return View(doctor);
    }

    private async void PopulateSpecialties()
    {
        var (specialties, _) = await _specialtyRepository.GetAllAsync(1, orderBy: s => s.Name);
        ViewBag.Specialities = new MultiSelectList(specialties, nameof(Speciality.Id), nameof(Speciality.Name));
    }

    private void RemoveModelStateProperties()
    {
        ModelState.Remove(nameof(Doctor.Appointments));
        ModelState.Remove(nameof(Doctor.Specialities));
    }
}