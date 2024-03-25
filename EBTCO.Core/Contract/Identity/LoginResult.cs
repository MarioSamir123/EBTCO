using Microsoft.AspNetCore.Identity;

namespace EBTCO.Core.Contract.Identity
{
    public class LoginResult
    {
        public required IdentityResult Result { get; set; }
        public String Token { get; set; } = String.Empty;
    }
}
