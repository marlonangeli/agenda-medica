using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Healthy.Web.Models;
using Healthy.Web.Constants;
using Healthy.Domain.Interfaces;
using Healthy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Web.Controllers;

public class HomeController : Controller
{
    private readonly IBaseRepository<Appointment> _repository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IBaseRepository<Appointment> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var query = _repository.GetQueryable().Include(i => i.Patient).Include(i => i.Doctor);
        var appointments = await query
            .Skip((page - 1) * ViewConstants.PageSize)
            .Take(ViewConstants.PageSize)
            .ToListAsync();
        return View(appointments);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
