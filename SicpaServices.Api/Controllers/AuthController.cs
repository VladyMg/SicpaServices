using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sicpa.Api.Application.Core.EmployeeLogic.Commands;
using System.Threading.Tasks;

namespace SicpaServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Login(EmployeeValidUser.EmployeeValidUserCommand command)
        {
            var resp = await mediator.Send(command);

            if (resp.ok)
            {
                return Ok(resp);
            }
            else
            {
                return Unauthorized(resp);
            }
        }
    }
}
