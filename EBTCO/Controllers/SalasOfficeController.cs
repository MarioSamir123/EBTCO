using EBTCO.Core.Api;
using EBTCO.Core.Features.SalesOffices.Commands.Add;
using EBTCO.Core.Features.SalesOffices.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBTCO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasOfficeController : BaseController
    {
        private readonly IMediator _mediator;

        public SalasOfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse<AddSalesOfficeCommandResponse>>> Add([FromBody] AddSalesOfficeCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpDelete]
        public async Task<ActionResult<APIResponse<DeleteSalesOfficeCommandResponse>>> Delete ([FromQuery] DeleteSalesOfficeCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
    }
}
