using Healthy.Domain.Entities;
using Healthy.Domain.Interfaces;
using Healthy.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Web.Controllers;

public class AppointmentController : Controller
{
    private readonly IBaseRepository<Appointment> _repository;

    public AppointmentController(IBaseRepository<Appointment> repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Details(int id)
    {
        var appointment = await _repository.GetQueryable()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.Id == id);

        return View(appointment);
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        // var (appointments, total) =
        //     await _repository.GetAllAsync(page: page, pageSize: ViewConstants.PageSize, includeAll: true);
        
        var query = _repository.GetQueryable()
            .Include(i => i.Patient)
            .Include(i => i.Doctor);
        var total = await query.CountAsync();
        var appointments = await query.Skip((page - 1) * ViewConstants.PageSize).Take(ViewConstants.PageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)total / ViewConstants.PageSize);

        return View(appointments);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        if (ModelState.IsValid)
        {
            await _repository.AddAsync(appointment);
            return RedirectToAction(nameof(Index));
        }

        return View(appointment);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var appointment = await _repository.GetByIdAsync(id, true);
        return View(appointment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Appointment appointment)
    {
        if (id != appointment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _repository.UpdateAsync(appointment);
            return RedirectToAction(nameof(Index));
        }

        return View(appointment);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _repository.GetByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}