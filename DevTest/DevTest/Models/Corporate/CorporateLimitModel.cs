using System.ComponentModel.DataAnnotations;
using DevTest.AppUtils;

namespace DevTest.Models.Corporate;

public class CorporateLimitModel : LimitCalculatorModel
{
    [Range(Constants.MIN_WEEKLY_WORKING_HOURS, Constants.MAX_WEEKLY_WORKING_HOURS, ErrorMessage = "Weekly working hours must be in range 0 to 38 per week")]
    public double WeeklyWorkingHours { get; set; }
}