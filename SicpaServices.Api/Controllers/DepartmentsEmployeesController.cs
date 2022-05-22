using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sicpa.Api.Application.Core.DepartamentsEmployeesLogic.Commands;
using Sicpa.Api.Application.Core.EmployeeLogic.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SicpaServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsEmployeesController : ControllerBase
    {
        private readonly IMediator mediator;

        public DepartmentsEmployeesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromHeader] string requestingUser, DepartmentEmployeeCreate.DepartmentEmployeeCreateCommand command)
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

        [HttpDelete("{id_department}/{id_employee}")]
        public async Task<ActionResult> Delete([FromHeader] string requestingUser, int id_department, int id_employee)
        {
            var access = await mediator.Send(new EmployeeValidUser.EmployeeValidUserCommand { user = requestingUser, position = "admin" });

            if (access.ok)
            {
                var resp = await mediator.Send(new DepartmentEmployeeDelete.DepartmentEmployeeDeleteCommand
                {
                    id_department = id_department,
                    id_employee = id_employee
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
