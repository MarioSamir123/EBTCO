using EBTCO.Core.Api;
using EBTCO.Core.Contract.Identity;
using MediatR;

namespace EBTCO.Core.Features.Identity.Commands.LoginByGoogle
{
    public class LoginByGoogleCommandHandler : IRequestHandler<LoginByGoogleCommand, APIResponse<LoginByGoogleCommandResponse>>
    {
        private readonly IGoogleAuthService _authService;
        private readonly IAuthorizeService _authorizeService;
        private readonly ITokenGenerator _tokenGenerator;
        public LoginByGoogleCommandHandler(IGoogleAuthService authService, IAuthorizeService authorizeService, ITokenGenerator tokenGenerator)
        {
            _authService = authService;
            _authorizeService = authorizeService;
            _tokenGenerator = tokenGenerator;

        }
        public async Task<APIResponse<LoginByGoogleCommandResponse>> Handle(LoginByGoogleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userData = await _authService.GetUserDataFromGoogleToken(request.googleToken);

                var user = await _authorizeService.GetUserByEmail(userData.Email.ToLower().Trim());
                if (user == null)
                {
                    String Password = "41%N13z3@Fui";
                    var result = await _authorizeService.CreateUser(userData.Email, Password);
                    if (result.Result.Succeeded)
                    {
                        return new APIResponse<LoginByGoogleCommandResponse>
                        {
                            Data = new LoginByGoogleCommandResponse(result.Token),
                            HttpStatusCode = System.Net.HttpStatusCode.OK,
                        };
                    }
                    else
                    {
                        return new APIResponse<LoginByGoogleCommandResponse>
                        {
                            Errors = new List<string> { "Something Wrong!" },
                            HttpStatusCode = System.Net.HttpStatusCode.Unauthorized,
                        };
                    }
                }

                var token = await _tokenGenerator.GenerateWebToken(user);

                return new APIResponse<LoginByGoogleCommandResponse>
                {
                    Data = new LoginByGoogleCommandResponse(token),
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                };
            }
            catch (Exception)
            {
                return new APIResponse<LoginByGoogleCommandResponse>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.Unauthorized,
                };
            }
            
        }
    }
}
