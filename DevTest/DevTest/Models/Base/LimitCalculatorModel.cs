using System.ComponentModel.DataAnnotations;
using DevTest.Models.enums;

namespace DevTest.Models;

public abstract class LimitCalculatorModel
{
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
    public double Salary { get; set; }
    [Required(ErrorMessage = "Employment type must be filled")]
    public EmploymentType EmploymentType { get; set; }
}