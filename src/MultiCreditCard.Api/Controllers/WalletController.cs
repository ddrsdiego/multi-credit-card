using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiCreditCard.Users.Application.Commands;
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

        public async Task<IActionResult> AddNewCreditCart([FromBody] RequestAddNewCreditCardCommand command)
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