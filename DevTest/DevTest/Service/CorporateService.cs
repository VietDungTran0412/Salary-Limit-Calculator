using DevTest.AppUtils;
using DevTest.Models.Corporate;
using DevTest.Models.enums;

namespace DevTest.Service;

/* Handling Corporate relevant tasks (Implementation */
public class CorporateService : ICorporateService
{
    private const double SalaryThreshold = 100_000; // Salary threshold
    private const double RateForAboveThreshold = 0.001; // Rate applied for salaries above the threshold
    private const double RateForBelowThreshold = 0.01; // Rate applied for salaries below the threshold

    private readonly ILogger<ICorporateService> _logger;

    public CorporateService(ILogger<ICorporateService> logger)
    {
        _logger = logger;
    }

    
    /* Implementation of Limit Calculator */
    public double CalculateLimit(CorporateLimitModel corporateDto)
    {
        double fullTimeLimit = GetFullTimeLimit(corporateDto.Salary);
        double limit = 0;
        _logger.LogInformation($"Request to calculate the limit with Employment Type: {corporateDto.EmploymentType}");
        switch (corporateDto.EmploymentType)
        {
            case EmploymentType.FULLTIME:
                limit = fullTimeLimit;
                break;
            case EmploymentType.PARTTIME:
                limit = fullTimeLimit * ( corporateDto.WeeklyWorkingHours / Constants.MAX_WEEKLY_WORKING_HOURS);
                break;
            default:
                // Base case
                return 0;
        }
        _logger.LogInformation($"The result of the limit is: {limit}");
        return Math.Round(limit, 3);
    }

    /* Get the full time salary */
    private double GetFullTimeLimit(double salary)
    {
        if (salary < SalaryThreshold)
        {
            return salary * RateForBelowThreshold;
        }

        return salary * RateForAboveThreshold;
    }
}