﻿using Healthy.Domain.Entities;
using Healthy.Domain.Interfaces;
using Healthy.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Web.Controllers;

public class SpecialityController : Controller
{
    private readonly IBaseRepository<Speciality> _repository;

    public SpecialityController(IBaseRepository<Speciality> repository)
    {
        _repository = repository;
    }
    
    public async Task<IActionResult> Index(int page = 1)
    {
        var (specialities, total) =
            await _repository.GetAllAsync(page, pageSize: ViewConstants.PageSize, orderBy: p => p.Name);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)total / ViewConstants.PageSize);

        return View(specialities);
    }
    
    public async Task<IActionResult> Details(int id, string? modelError = null)
    {
        var speciality = await _repository.GetQueryable()
            .Include(i => i.Doctors)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (modelError is not null)
        {
            ModelState.AddModelError(string.Empty, modelError);
        }
        
        return View(speciality);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Speciality speciality)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(speciality);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
        }
        
        return View(speciality);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var speciality = await _repository.GetByIdAsync(id);
        
        return View(speciality);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Speciality speciality)
    {
        try
        {
            if (id != speciality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(speciality);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
        }
        
        return View(speciality);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return RedirectToAction(nameof(Details), new {id = id, modelError = e.Message});
        }
    }
}