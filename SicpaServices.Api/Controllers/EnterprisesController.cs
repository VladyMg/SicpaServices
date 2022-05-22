using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sicpa.Api.Application.Core.EmployeeLogic.Commands;
using Sicpa.Api.Application.Core.EnterpriseLogic.Commands;
using Sicpa.Api.Application.Core.EnterpriseLogic.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SicpaServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterprisesController : ControllerBase
    {
        private readonly IMediator mediator;

        public EnterprisesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromHeader] string requestingUser, EnterpriseCreate.EnterpriseCreateCommand command)
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
            var resp = await mediator.Send(new EnterpriseList.EnterpriseListQuery());

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
        public async Task<ActionResult> Update([FromHeader] string requestingUser, EnterpriseUpdate.EnterpriseUpdateCommand command)
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
            var resp = await mediator.Send(new EnterpriseFilterId.EnterpriseFilterIdQuery
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
                var resp = await mediator.Send(new EnterpriseDelete.EnterpriseDeleteCommand
                {
                    id = id,
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
