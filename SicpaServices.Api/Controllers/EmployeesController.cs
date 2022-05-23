using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sicpa.Api.Application.Core.EmployeeLogic.Commands;
using Sicpa.Api.Application.Core.EmployeeLogic.Queries;
using System.Threading.Tasks;

namespace SicpaServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromHeader] string requestingUser, EmployeeCreate.EmployeeCreateCommand command)
        {
            var access = await mediator.Send(new EmployeeValidUser.EmployeeValidUserCommand { user = requestingUser, position = "admin" });

            if (access.ok)
            {
                command.requestingUser = requestingUser;

                var resp = await mediator.Send(command);

                if (resp.ok)
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest(resp);
                }
            }
            else
            {
                return Unauthorized(access);
            }
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            var resp = await mediator.Send(new EmployeeList.EmployeeListQuery());

            if (resp.ok)
            {
                return Ok(resp);
            }
            else
            {
                return BadRequest(resp);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromHeader] string requestingUser, EmployeeUpdate.EmployeeUpdateCommand command)
        {
            var access = await mediator.Send(new EmployeeValidUser.EmployeeValidUserCommand { user = requestingUser, position = "admin" });

            if (access.ok)
            {
                command.requestingUser = requestingUser;

                var resp = await mediator.Send(command);

                if (resp.ok)
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest(resp);
                }
            }
            else
            {
                return Unauthorized(access);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FilterById(int id)
        {
            var resp = await mediator.Send(new EmployeeFilterId.EmployeeFilterIdQuery
            {
                id = id
            });

            if (resp.ok)
            {
                return Ok(resp);
            }
            else
            {
                return BadRequest(resp);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromHeader] string requestingUser, int id)
        {
            var access = await mediator.Send(new EmployeeValidUser.EmployeeValidUserCommand { user = requestingUser, position = "admin" });

            if (access.ok)
            {

                var resp = await mediator.Send(new EmployeeDelete.EmployeeDeleteCommand
                {
                    id = id
                });

                if (resp.ok)
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest(resp);
                }
            }
            else
            {
                return Unauthorized(access);
            }
        }

    }
}
