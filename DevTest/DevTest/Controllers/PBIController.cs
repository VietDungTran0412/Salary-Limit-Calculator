using DevTest.Models;
using DevTest.Service;
using Microsoft.AspNetCore.Mvc;

namespace DevTest.Controllers;


/* Controller for PBI View */
public class PBIController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPBIService _pbiService;

    public PBIController(ILogger<HomeController> logger, IPBIService pbiService)
    {
        _logger = logger;
        _pbiService = pbiService;
    }

    [HttpPost]
    public IActionResult Index(EducationLimitModel inputModel)
    {
        _logger.LogInformation("Retrieve request to calculate the package limit from a PBI employee");
        // Validate if the input model entries match the specified requirements
        if (!ModelState.IsValid)
        {
            _logger.LogError("Validation failed: {Errors}", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));;
            return View("Index", new LimitResultModel() { Limit = 0 });
        }

        double limit = _pbiService.CalculateLimit(inputModel);
        return View("Index", new LimitResultModel() { Limit = limit }); 
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        _logger.LogInformation("Retrieving request access site for PBI employee");
        return View();
    }
}