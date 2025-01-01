using DevTest.Models;
using DevTest.Models.enums;
using DevTest.Service;
using Microsoft.Extensions.Logging.Abstractions;

namespace UnitTesting.Service;

[TestFixture]
public class PBIServiceTest
{
    private IPBIService _pbiService;
    [SetUp]
    public void SetUp()
    {
        // Null Logger for testing purpose
        var logger = NullLogger<PBIService>.Instance;
        _pbiService = new PBIService(logger);
    }

    [Test(Description = "Test Casual Employee in PBI")]
    [TestCase(true, 100_000, EmploymentType.CASUAL, 13000)] // (100000 * 10%) + (2000 + 100000 * 1%)
    [TestCase(false, 100_000, EmploymentType.CASUAL, 10000)] // (100000 * 10%)
    [TestCase(true, 1_000_000, EmploymentType.CASUAL, 112000)] // 1,000,000 * 10% + (2000 + 1,000,000 * 1%)
    public void CalculateCasualLimit_ShouldReturn10PercentSalaryPlusEducationRate(bool isEducated, double salary, EmploymentType employmentType, double expectedLimit)
    {
        var inputModel = new EducationLimitModel()
            { Salary = salary, HavingMinDegree = isEducated, EmploymentType = employmentType };
        double limit = _pbiService.CalculateLimit(inputModel);
        Assert.That(limit, Is.EqualTo(expectedLimit));
    }
    
    [Test(Description = "Test Part-Time Employee in PBI")]
    [TestCase(true, 100_000, EmploymentType.PARTTIME, 28440)] // ((100000 * 32.55%) + (2000 + 100000 * 1%)) * 80%
    [TestCase(true, 1_000_000, EmploymentType.PARTTIME, 49600)] // (50,000 + (2000 + 1000000 * 1%)) * 80%
    [TestCase(false, 100_000, EmploymentType.PARTTIME, 26040)] // ((100000 * 32.55%) * 80%
    // [TestCase(true, 1_000_000, EmploymentType.PARTTIME, 112000)] // 1,000,000 * 10% + (2000 + 1,000,000 * 1%)
    public void CalculatePartTimeLimit_ShouldBaseLimitPlusEducationRate(bool isEducated, double salary, EmploymentType employmentType, double expectedLimit)
    {
        var inputModel = new EducationLimitModel()
            { Salary = salary, HavingMinDegree = isEducated, EmploymentType = employmentType };
        double limit = _pbiService.CalculateLimit(inputModel);
        Assert.That(limit, Is.EqualTo(expectedLimit));
    }
    
    [Test(Description = "Test Full-Time Employee in PBI")]
    [TestCase(true, 100_000, EmploymentType.FULLTIME, 35550)] // ((100000 * 32.55%) + (2000 + 100000 * 1%))
    [TestCase(true, 1_000_000, EmploymentType.FULLTIME, 62000)] // (50,000 + (2000 + 1000000 * 1%))
    [TestCase(false, 100_000, EmploymentType.FULLTIME, 32550)] // (100000 * 32.55%)
    public void CalculateFullTimeLimit_ShouldBaseLimitPlusEducationRate(bool isEducated, double salary, EmploymentType employmentType, double expectedLimit)
    {
        var inputModel = new EducationLimitModel()
            { Salary = salary, HavingMinDegree = isEducated, EmploymentType = employmentType };
        double limit = _pbiService.CalculateLimit(inputModel);
        Assert.That(limit, Is.EqualTo(expectedLimit));
    }
}