using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiCreditCard.Users.Application.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Api.Controllers
{
    [Route("api/credit-cards")]
    public class CreditCardController : Controller
    {
        private readonly IMediator _mediator;

        public CreditCardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewCreditCard([FromBody] RequestAddNewCreditCardCommand command)
        {
            if (command == null)
                return BadRequest();

            var response = await _mediator.Send(command);
            if (response.HasError)
                return BadRequest(new { errors = response.Errors.Select(x => x) });

            return Created("", null);
        }
    }
}