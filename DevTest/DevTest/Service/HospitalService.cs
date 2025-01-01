using DevTest.Models;
using DevTest.Models.enums;

namespace DevTest.Service;

public class HospitalService : IHospitalService
{
    private const double FlatLimit = 10_000; // The flat value of $10000
    private const double MaxBaseBonusLimit = 30_000; // The salary threshold for deciding the rate
    private const double SalaryBonusRate = 0.20; // Base rate
    private const double EducationBonus = 5000; // The bonus if obtained a minimum degree
    private const double FullTimeIncreaseRate = 0.095; // Their package limit is increased by 9.5%
    private const double AdditionalSalaryRate = 0.012;
    
    private readonly ILogger<IHospitalService> _logger;

    public HospitalService(ILogger<HospitalService> logger)
    {
        _logger = logger;
    }
    
    public double CalculateLimit(EducationLimitModel inputModel)
    {
        double limit = GetBaseLimit(inputModel.Salary); // Base limit bonus
        _logger.LogInformation($"Retrieve the base limit with: {limit}");
        // Add education bonus
        if (inputModel.HavingMinDegree) limit += EducationBonus;
        if (inputModel.EmploymentType == EmploymentType.FULLTIME)
        {
            limit += limit * FullTimeIncreaseRate + inputModel.Salary * AdditionalSalaryRate;
            _logger.LogInformation($"Increase the base limit for full-time workers with: {limit}");
        }
        return limit;
    }

    private double GetBaseLimit(double salary)
    {
        double baseBonus = salary * SalaryBonusRate;
        // The greater value between either a flat $10,000 or 20% of their salary up to $30,000
        baseBonus = baseBonus > MaxBaseBonusLimit ? MaxBaseBonusLimit : Math.Max(baseBonus, FlatLimit);
        return baseBonus;
    }
}