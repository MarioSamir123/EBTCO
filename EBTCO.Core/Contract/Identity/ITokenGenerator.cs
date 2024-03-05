using EBTCO.Domain.Identity;

namespace EBTCO.Core.Contract.Identity
{
    public interface ITokenGenerator
    {
        Task<string> GenerateWebToken(User User);
    }
}
