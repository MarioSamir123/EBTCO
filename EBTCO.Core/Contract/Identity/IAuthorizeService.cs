using EBTCO.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace EBTCO.Core.Contract.Identity
{
    public interface IAuthorizeService
    {
        Task<LoginResult> Login(String email, String Password);
        Task<LoginResult> CreateUser(String email, String Password);
    }
}
