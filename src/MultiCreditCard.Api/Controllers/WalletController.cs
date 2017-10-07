using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiCreditCard.Users.Command.Commands;
using MultiCreditCard.Wallets.Application.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Api.Controllers
{
    [Route("api/wallets")]
    public class WalletController : Controller
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [Route("buy")]
        public async Task<IActionResult> RequestCreditCardBuy([FromBody] RequestCreditCardBuyCommand command)
        {
            if (command == null)
                return BadRequest();

            var response = await _mediator.Send(command);

            if (response.HasError)
                return BadRequest(new { errors = response.Errors.Select(x => x) });

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("update-limit")]
        public async Task<IActionResult> RequestUpdateUserCreditLimit([FromBody] RequestUpdateUserCreditLimitCommand command)
        {
            if (command == null)
                return BadRequest();

            var response = await _mediator.Send(command);

            if (response.HasError)
                return BadRequest(new { errors = response.Errors.Select(x => x) });

            return Ok();
        }
    }
}