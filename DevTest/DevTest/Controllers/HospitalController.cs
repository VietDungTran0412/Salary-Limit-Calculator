using DevTest.Models;
using DevTest.Service;
using Microsoft.AspNetCore.Mvc;

namespace DevTest.Controllers;

public class HospitalController : Controller
{
    private readonly ILogger<HospitalController> _logger;
    private readonly IHospitalService _hospitalService;

    public HospitalController(ILogger<HospitalController> logger, IHospitalService hospitalService)
    {
        _logger = logger;
        _hospitalService = hospitalService;
    }

    [HttpPost]
    public IActionResult Index(EducationLimitModel inputModel)
    {
        _logger.LogInformation("Retrieving request to calculate the limit for hospital employee");
        // If the input model is not valid then return limit  = 0
        if (!ModelState.IsValid)
        {
            _logger.LogError("Validation failed: {Errors}", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));;
            return View("Index", new LimitResultModel() { Limit = 0 });
        }
        _logger.LogInformation("Start calculating the limit for hospital employee");
        var limit = _hospitalService.CalculateLimit(inputModel);
        return View("Index", new LimitResultModel() { Limit = limit });
    }

    [HttpGet]
    public IActionResult Index()
    {
        _logger.LogInformation("Retrieving request access site for hospital employee");
        return View();
    }
}