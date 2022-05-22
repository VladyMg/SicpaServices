using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sicpa.Api.Application.Core.DepartmentLogic.Commands;
using Sicpa.Api.Application.Core.DepartmentLogic.Queries;
using Sicpa.Api.Application.Core.EmployeeLogic.Commands;
using System.Threading.Tasks;

namespace SicpaServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public DepartmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromHeader] string requestingUser, DepartmentCreate.DepartmentCreateCommand command)
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
            var resp = await mediator.Send(new DepartmentList.DepartmentListQuery());

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
        public async Task<ActionResult> Update([FromHeader] string requestingUser, DepartmentUpdate.DepartmentUpdateCommand command)
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
            var resp = await mediator.Send(new DepartmentFilterId.DepartmentFilterIdQuery
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
                var resp = await mediator.Send(new DepartmentDelete.DepartmentDeleteCommand
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
