using EBTCO.Domain.Identity;

namespace EBTCO.Core.Contract.Identity
{
    public interface IAuthorizeService
    {
        Task<LoginResult> Login(String email, String Password);
        Task<User?> GetUserByEmail(String email);
        Task<LoginResult> CreateUser(String email, String Password);
    }
}
