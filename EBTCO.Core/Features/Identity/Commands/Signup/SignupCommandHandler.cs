using EBTCO.Core.Api;
using EBTCO.Core.Contract.Identity;
using MediatR;

namespace EBTCO.Core.Features.Identity.Commands.Signup
{
    public class SignupCommandHandler : IRequestHandler<SignupCommand, APIResponse<SignupCommandResponse>>
    {
        private readonly IAuthorizeService _authorizeService;
        public SignupCommandHandler(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        public async Task<APIResponse<SignupCommandResponse>> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizeService.CreateUser(request.email , request.password);
            if (result.Result.Succeeded)
            {
                return new APIResponse<SignupCommandResponse>
                {
                    Data = new SignupCommandResponse(result.Token),
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                };
            }
            return new APIResponse<SignupCommandResponse>
            {
                Errors = result.Result.Errors.Select(row => row.Description).ToList(),
                HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
            };
        }
    }
}
