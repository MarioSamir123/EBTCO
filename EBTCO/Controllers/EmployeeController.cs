using EBTCO.Core.Api;
using EBTCO.Core.Features.Employees.Commands.Add;
using EBTCO.Core.Features.Employees.Commands.Delete;
using EBTCO.Core.Features.Employees.Commands.Edit;
using EBTCO.Core.Features.Employees.Queries.GetAll;
using EBTCO.Core.Features.Employees.Queries.GetById;
using EBTCO.Core.Features.Employees.Queries.GetEmployeesNames;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBTCO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<APIResponse<AddEmployeeCommandResponse>>> Add([FromBody] AddEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpPut]
        public async Task<ActionResult<APIResponse<EditEmployeeCommandResponse>>> Edit([FromBody] EditEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpDelete]
        public async Task<ActionResult<APIResponse<DeleteEmployeeCommandResponse>>> Delete([FromQuery] DeleteEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpGet]
        public async Task<ActionResult<APIResponse<GetAllEmployeesQueryResponse>>> GetAll([FromQuery] GetAllEmployeesQuery query)
        {
            var result = await _mediator.Send(query);
            return GetApiResponse(result);
        }
        [HttpGet("GetOne")]
        public async Task<ActionResult<APIResponse<GetEmployeeByIdQueryResponse>>> GetByID([FromQuery] GetEmployeeByIdQuery query)
        {
            var result = await _mediator.Send(query);
            return GetApiResponse(result);
        }
        [HttpGet("Names")]
        public async Task<ActionResult<APIResponse<GetEmployeesNamesQueryResponse>>> GetEmployeesNames([FromQuery] GetEmployeesNamesQuery query)
        {
            var result = await _mediator.Send(query);
            return GetApiResponse(result);
        }
    }
}
