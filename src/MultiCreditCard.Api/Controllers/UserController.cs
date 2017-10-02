using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiCreditCard.Users.Command.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Api.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser([FromBody]RegisterNewUserCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var response = await  _mediator.Send(command);

            if (response.HasError)
            {
                return BadRequest(new
                {
                    errors = response.Errors.Select(x => x)
                });
            }

            return Created("", command);
        }
    }
}
