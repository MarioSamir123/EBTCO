using EBTCO.Core.Api;
using EBTCO.Core.Features.Owners.Commands.Add;
using EBTCO.Core.Features.Owners.Commands.Delete;
using EBTCO.Core.Features.Owners.Commands.Edit;
using EBTCO.Core.Features.Owners.Queries.GetAll;
using EBTCO.Core.Features.Owners.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBTCO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : BaseController
    {
        private readonly IMediator _mediator;

        public OwnerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<APIResponse<AddOwnerCommandResponse>>> Add([FromBody] AddOwnerCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpPut]
        public async Task<ActionResult<APIResponse<EditOwnerCommandResponse>>> Edit([FromBody] EditOwnerCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpDelete]
        public async Task<ActionResult<APIResponse<DeleteOwnerCommandResponse>>> Delete([FromQuery] DeleteOwnerCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpGet]
        public async Task<ActionResult<APIResponse<GetAllOwnersQueryResponse>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllOwnersQuery());
            return GetApiResponse(result);
        }
        [HttpGet("GetOne")]
        public async Task<ActionResult<APIResponse<GetOwnerByIdQueryResponse>>> GetByID([FromQuery] GetOwnerByIdQuery query)
        {
            var result = await _mediator.Send(query);
            return GetApiResponse(result);
        }
    }
}
