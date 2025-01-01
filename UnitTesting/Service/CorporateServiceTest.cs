using DevTest.AppUtils;
using DevTest.Models.Corporate;
using DevTest.Models.enums;
using DevTest.Service;
using Microsoft.Extensions.Logging.Abstractions;

namespace UnitTesting.Service;

[TestFixture]
public class CorporateServiceTest
{
    private ICorporateService _corporateService;
    [SetUp]
    public void SetUp()
    {
        var logger = NullLogger<ICorporateService>.Instance;
        _corporateService = new CorporateService(logger);
    }

    [Test(Description = "Test casual -- Should return 0")]
    [TestCase(20,100_000, EmploymentType.CASUAL, 0)]
    [TestCase(12,200_000, EmploymentType.CASUAL, 0)]
    public void CalculateCasualLimit_Return0(double weeklyWorkingHours, double salary, EmploymentType employmentType, double expectedLimit)
    {
        var inputModel = new CorporateLimitModel()
            { WeeklyWorkingHours = weeklyWorkingHours, Salary = salary, EmploymentType = employmentType };
        double limit = _corporateService.CalculateLimit(inputModel);
        Assert.That(limit, Is.EqualTo(expectedLimit));
    }

    [Test(Description =
        "Test Part-Time Employee - should return half of the limit")]
    [TestCase(Constants.MAX_WEEKLY_WORKING_HOURS / 2, 10_000, EmploymentType.PARTTIME, 50)]
    [TestCase(Constants.MAX_WEEKLY_WORKING_HOURS / 2, 200_000, EmploymentType.PARTTIME, 100)]
    [TestCase(Constants.MAX_WEEKLY_WORKING_HOURS, 200_000, EmploymentType.PARTTIME, 200)]
    [TestCase(Constants.MIN_WEEKLY_WORKING_HOURS, 200_000, EmploymentType.PARTTIME, 0)]
    public void CalculatePartTimeLimit_ReturnTheCorrespondingLimit(double weeklyWorkingHours, double salary,
        EmploymentType employmentType, double expectedLimit)
    {
        var inputModel = new CorporateLimitModel()
            { WeeklyWorkingHours = weeklyWorkingHours, Salary = salary, EmploymentType = employmentType };
        double limit = _corporateService.CalculateLimit(inputModel);
        Assert.That(limit, Is.EqualTo(expectedLimit));
    }
    
    [Test(Description = "Test Full-Time Employee - Should return full amount of limit")]
    [TestCase(Constants.MAX_WEEKLY_WORKING_HOURS / 2, 10_000, EmploymentType.FULLTIME, 100)]
    [TestCase(Constants.MAX_WEEKLY_WORKING_HOURS, 200_000, EmploymentType.FULLTIME, 200)]
    [TestCase(Constants.MAX_WEEKLY_WORKING_HOURS, 100_000, EmploymentType.FULLTIME, 100)]
    public void CalculateFullTimeLimit_ReturnTheCorrespondingLimit(double weeklyWorkingHours, double salary,
        EmploymentType employmentType, double expectedLimit)
    {
        var inputModel = new CorporateLimitModel()
            { WeeklyWorkingHours = weeklyWorkingHours, Salary = salary, EmploymentType = employmentType };
        double limit = _corporateService.CalculateLimit(inputModel);
        Assert.That(limit, Is.EqualTo(expectedLimit));
    }
}