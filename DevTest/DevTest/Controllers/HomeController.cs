using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevTest.Models;
using DevTest.Models.enums;

namespace DevTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult StartCalculate(Company companyType)
    {
        _logger.LogInformation($"Received request to redirect to {companyType}");
        switch (companyType)
        {
            case Company.CORPORATE:
                return RedirectToAction("Index", "Corporate");
            case Company.PBI:
                return RedirectToAction("Index", "PBI");
            case Company.HOSPITAL:
                return RedirectToAction("Index", "Hospital");
            default:
                return RedirectToAction("Index", "Home");
        }
    }
    
    public IActionResult Index()
    {
        _logger.Log(LogLevel.Information, "Accessing the index page");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}