using EBTCO.Core.Contract.Identity;
using EBTCO.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EBTCO.DB.Identity
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        public AuthorizeService(UserManager<User> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResult> CreateUser(String email, String Password)
        {
            var user = new User()
            {
                Email = email,
                UserName = email,
            };
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                var token = await _tokenGenerator.GenerateWebToken(user);
                return new LoginResult
                {
                    Result = IdentityResult.Success,
                    Token = token
                };
            }
            return new LoginResult { Result = result };
        }

        public async Task<LoginResult> Login(string email, string Password)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(row => row.Email !=null && row.Email.ToLower().Equals(email.ToLower()));

            if (user == null)
            {
                var error = new IdentityError { Description = "Invalid Login!" };
                return new LoginResult()
                {
                    Result = IdentityResult.Failed(error)
                };
            }
            var token = await _tokenGenerator.GenerateWebToken(user);
            return new LoginResult()
            {
                Result = IdentityResult.Success,
                Token = token
            };
        }
    }
}
