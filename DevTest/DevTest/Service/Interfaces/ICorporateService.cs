using DevTest.Models.Corporate;
using DevTest.Models.enums;

namespace DevTest.Service;

public interface ICorporateService
{
    public double CalculateLimit(CorporateLimitModel corporateDto);
}