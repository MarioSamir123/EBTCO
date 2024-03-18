using EBTCO.Core.Api;
using EBTCO.Core.Features.Properties.Commands.AddProperty;
using EBTCO.Core.Features.Properties.Commands.OwnProperty;
using EBTCO.Core.Features.Properties.Queries.GetProperties;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBTCO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : BaseController
    {
        private readonly IMediator _mediator;
        public PropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<APIResponse<AddPropertyCommandResponse>>> Add([FromBody] AddPropertyCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpPost("Sale")]
        public async Task<ActionResult<APIResponse<OwnPropertyCommandResponse>>> Sale([FromBody] OwnPropertyCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpGet("GetPerOfficer")]
        public async Task<ActionResult<APIResponse<GetPropertiesQueryResponse>>> GetProperties([FromQuery] GetPropertiesQuery query)
        {
            var result = await _mediator.Send(query);
            return GetApiResponse(result);
        }
    }
}
