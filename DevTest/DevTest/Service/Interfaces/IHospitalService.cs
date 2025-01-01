using DevTest.Models;

namespace DevTest.Service;

public interface IHospitalService
{
    public double CalculateLimit(EducationLimitModel inputModel);
}