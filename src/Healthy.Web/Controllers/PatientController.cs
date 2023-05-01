﻿using Healthy.Domain.Entities;
using Healthy.Domain.Interfaces;
using Healthy.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Web.Controllers;

public class PatientController : Controller
{
    private readonly IBaseRepository<Patient> _repository;

    public PatientController(IBaseRepository<Patient> repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var (patients, total) =
            await _repository.GetAllAsync(page, pageSize: ViewConstants.PageSize, orderBy: p => p.FirstName);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)total / ViewConstants.PageSize);

        return View(patients);
    }

    public async Task<IActionResult> Details(int id)
    {
        // TODO - Implementar o IncludeAll
        // var patient = await _repository.GetByIdAsync(id, true);
        var patient = await _repository.GetQueryable()
            .Include(i => i.Appointments)
            .ThenInclude(i => i.Doctor)
            .FirstOrDefaultAsync(f => f.Id == id);

        return View(patient);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Patient patient)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(patient);
                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.InnerException?.Message);
            return View(patient);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var patient = await _repository.GetByIdAsync(id);
        
        return View(patient);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Patient patient)
    {
        try
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(patient);
                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(patient);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}