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

    public async Task<IActionResult> Details(int id)
    {
        // TODO - Implementar o IncludeAll
        // var doctor = await _repository.GetByIdAsync(id, true);
        var doctor = await _doctorRepository.GetQueryable()
            .Include(i => i.Appointments)
            .ThenInclude(i => i.Patient)
            .Include(i => i.Specialities)
            .FirstOrDefaultAsync(f => f.Id == id);

        return View(doctor);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _doctorRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
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
        RemoveModelStateProperties();

        if (ModelState.IsValid)
        {
            var doctorEntity = doctor.Map();
            await _doctorRepository.AddAsync(doctorEntity);
            return RedirectToAction(nameof(Index));
        }

        PopulateSpecialties();
        
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