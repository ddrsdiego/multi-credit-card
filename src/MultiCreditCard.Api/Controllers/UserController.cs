using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiCreditCard.Users.Command.Commands;
using System;
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
        [AllowAnonymous]
        public async Task<IActionResult> RegisterNewUser([FromBody]RegisterNewUserCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);

            if (response.HasError)
                return BadRequest(new { errors = response.Errors.Select(x => x) });

            return Created("", new
            {
                response.UserId,
                response.UserName,
                response.Email,
                response.DocumentNumber
            });
        }

        [HttpGet]
        [Authorize]
        [Route("credit-cards")]
        public async Task<IActionResult> GetCreditCardsUser()
        {
            try
            {
                var response = await _mediator.Send(new GetCreditCardsUserCommand(User.FindFirst("access_token")?.Value));

                if (response.HasError)
                    return BadRequest(new { errors = response.Errors.Select(x => x) });

                return Ok(new
                {
                    response.UserId,
                    response.CreditLimitWallet,
                    response.DataTimeQuery,
                    CreditCards = response.CreditCards.ToArray()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
