using DevTest.Models;
using DevTest.Models.enums;
using DevTest.Service;
using Microsoft.Extensions.Logging.Abstractions;

namespace UnitTesting.Service;

[TestFixture]
public class HospitalServiceTest
{
    private IHospitalService _hospitalService;
    [SetUp]
    public void SetUp()
    {
        var logger = NullLogger<HospitalService>.Instance;
        _hospitalService = new HospitalService(logger);
    }

    [Test(Description = "Test Educated Non-FullTime Employee working in Hospital sector")]
    [TestCase(true, 40_000, EmploymentType.CASUAL, 15_000)]
    [TestCase(true, 100_000, EmploymentType.PARTTIME, 25_000)]
    [TestCase(true, 200_000, EmploymentType.CASUAL, 35_000)] // Casual Employee with $200,000 and educated
    public void CalculateEducatedNonFullTimeLimit_ShouldReturnBaseSalaryPlusEducationBonus(bool isEducated,
        double salary, EmploymentType employmentType, double expectedLimit)
    {
        var inputModel = new EducationLimitModel()
            { Salary = salary, HavingMinDegree = isEducated, EmploymentType = employmentType };
        double limit = _hospitalService.CalculateLimit(inputModel);
        Assert.That(limit, Is.EqualTo(expectedLimit));
    }
    
    [Test(Description = "Test Non-Educated Non-FullTime Employee working in Hospital sector")]
    [TestCase(false, 40_000, EmploymentType.CASUAL, 10_000)] // 10000
    [TestCase(false, 100_000, EmploymentType.PARTTIME, 20_000)]
    [TestCase(false, 200_000, EmploymentType.CASUAL, 30_000)] // Casual Employee with $200,000 and non-educated
    public void CalculateNonEducatedNonFullTimeLimit_ShouldReturnBaseSalaryPlusEducationBonus(bool isEducated,
        double salary, EmploymentType employmentType, double expectedLimit)
    {
        var inputModel = new EducationLimitModel()
            { Salary = salary, HavingMinDegree = isEducated, EmploymentType = employmentType };
        double limit = _hospitalService.CalculateLimit(inputModel);
        Assert.That(limit, Is.EqualTo(expectedLimit));
    }
    
    [Test(Description = "Test FullTime Employee working in Hospital sector")]
    [TestCase(true, 40_000, EmploymentType.FULLTIME, 16905)] // $ = (10000 + 5000)  * 1.095 + 40000 * 0.012 
    [TestCase(false, 40_000, EmploymentType.FULLTIME, 11430)] // $ = (10000)  * 1.095 + 40000 * 0.012
    // [TestCase(false, 200_000, EmploymentType.FULLTIME, 30_000)] // Casual Employee with $200,000 and non-educated
    public void CalculateFullTimeLimit_ShouldReturnBaseSalaryPlusEducationBonus(bool isEducated,
        double salary, EmploymentType employmentType, double expectedLimit)
    {
        var inputModel = new EducationLimitModel()
            { Salary = salary, HavingMinDegree = isEducated, EmploymentType = employmentType };
        double limit = _hospitalService.CalculateLimit(inputModel);
        Assert.That(limit, Is.EqualTo(expectedLimit));
    }
}