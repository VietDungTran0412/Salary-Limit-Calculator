using System.Diagnostics;
using DevTest.Models;
using DevTest.Models.Corporate;
using DevTest.Service;
using Microsoft.AspNetCore.Mvc;

namespace DevTest.Controllers;

/* Controller to handle relevant tasks for corporate employees */
public class CorporateController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICorporateService _corporateService;

    public CorporateController(ILogger<HomeController> logger, ICorporateService corporateService)
    {
        _logger = logger;
        _corporateService = corporateService;
    }

    [HttpPost]
    public IActionResult Index(CorporateLimitModel input)
    {
        _logger.LogInformation("Retrieve request to calculate the package limit from a Corporate employee");
        // Check the input if it is not valid
        if (!ModelState.IsValid)
        {
            _logger.LogError("Validation failed: {Errors}", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));;
            return View("Index", new LimitResultModel() { Limit = 0 });
        }

        var limit = _corporateService.CalculateLimit(input);
        _logger.LogInformation("Request to calculate the corporate limit value");
        return View("Index", new LimitResultModel() { Limit = limit });
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        _logger.Log(LogLevel.Information, "Accessing the index page");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}