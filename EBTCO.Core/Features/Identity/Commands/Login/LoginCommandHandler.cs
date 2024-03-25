using EBTCO.Core.Api;
using EBTCO.Core.Contract.Identity;
using MediatR;

namespace EBTCO.Core.Features.Identity.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, APIResponse<LoginCommandResponse>>
    {
        private readonly IAuthorizeService _authorizeService;

        public LoginCommandHandler(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        public async Task<APIResponse<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizeService.Login(request.email, request.password);
            if (result.Result.Succeeded) 
            { 
                return new APIResponse<LoginCommandResponse> 
                { 
                    HttpStatusCode = System.Net.HttpStatusCode.OK, 
                    Data = new LoginCommandResponse(result.Token)
                };
            }
            return new APIResponse<LoginCommandResponse>
            {
                HttpStatusCode = System.Net.HttpStatusCode.Unauthorized,
                Errors = result.Result.Errors.Select(row => row.Description).ToList()
            };
        }
    }
}
