using System.ComponentModel.DataAnnotations;

namespace DevTest.Models;

public class EducationLimitModel : LimitCalculatorModel
{
    public bool HavingMinDegree { get; set; } = false; // If having minimum degree requirement
}