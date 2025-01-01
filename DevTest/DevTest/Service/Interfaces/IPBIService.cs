using DevTest.Models;

namespace DevTest.Service;

public interface IPBIService
{
    public double CalculateLimit(EducationLimitModel inputModel);
}