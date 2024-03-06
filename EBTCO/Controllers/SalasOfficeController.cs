using EBTCO.Core.Api;
using EBTCO.Core.Features.SalesOffices.Commands.Add;
using EBTCO.Core.Features.SalesOffices.Commands.Delete;
using EBTCO.Core.Features.SalesOffices.Commands.Edit;
using EBTCO.Core.Features.SalesOffices.Commands.HireManager;
using EBTCO.Core.Features.SalesOffices.Queries.GetAll;
using EBTCO.Core.Features.SalesOffices.Queries.GetById;
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
        public async Task<ActionResult<APIResponse<DeleteSalesOfficeCommandResponse>>> Delete([FromQuery] DeleteSalesOfficeCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpPut]
        public async Task<ActionResult<APIResponse<EditSalesOfficeCommandResponse>>> Edit([FromBody] EditSalesOfficeCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpPut("HireManager")]
        public async Task<ActionResult<APIResponse<HireManagerCommandResponse>>> HireManager([FromBody] HireManagerCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpGet("GetOne")]
        public async Task<ActionResult<APIResponse<GetSalseOfficeByIdQueryResponse>>> GetByID([FromQuery] GetSalseOfficeByIdQuery query)
        {
            var result = await _mediator.Send(query);
            return GetApiResponse(result);
        }
        [HttpGet]
        public async Task<ActionResult<APIResponse<GetAllSalesOfficeQueryResponse>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllSalesOfficeQuery());
            return GetApiResponse(result);
        }
    }
}
