using System.ComponentModel;

namespace DevTest.Models.enums;

public enum EmploymentType
{
    [Description("Casual")]
    CASUAL,
    [Description("Full-Time")]
    FULLTIME,
    [Description("Part-Time")]
    PARTTIME
}