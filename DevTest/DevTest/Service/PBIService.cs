using DevTest.Models;
using DevTest.Models.enums;

namespace DevTest.Service;

public class PBIService : IPBIService
{
    private const double BaseSalaryBonus = 50_000; // Base bonus of 50,000
    private const double BaseSalaryBonusRate = 0.3255; // Base rate of 32.55% of the salary
    private const double CasualFlatRate = 0.1;
    private const double EducationBonusBase = 2000;
    private const double EducationBonusExtraRate = 0.01;
    private const double PartTimeRateOverFullTime = 0.8; // Received 80% of fulltime limit

    private readonly ILogger<IPBIService> _logger;

    public PBIService(ILogger<PBIService> logger)
    {
        _logger = logger;
    }
    
    public double CalculateLimit(EducationLimitModel inputModel)
    {
        double limit = 0;
        _logger.LogInformation($"Request to calculate the limit for PBI Employee with Employment Type: {inputModel.EmploymentType}");
        // Add the education bonus
        if (inputModel.HavingMinDegree) limit += GetEducationBonus(inputModel.Salary);
        // Get the limit if the employee is casual
        if (inputModel.EmploymentType == EmploymentType.CASUAL)
        {
            _logger.LogInformation("PBI Employee is a casual");
            limit += inputModel.Salary * CasualFlatRate;
            return limit;
        }
        // Add the base rate either $50,000 or 32.55%
        limit += Math.Min(BaseSalaryBonus, inputModel.Salary * BaseSalaryBonusRate);
        
        return inputModel.EmploymentType == EmploymentType.PARTTIME ? limit * PartTimeRateOverFullTime : limit;
    }

    /* Get the education bonus based on the salary */
    private double GetEducationBonus(double salary)
    {
        return EducationBonusBase + EducationBonusExtraRate * salary;
    }
    
}