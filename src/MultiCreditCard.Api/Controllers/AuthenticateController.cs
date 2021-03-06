﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiCreditCard.Users.Command.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Api.Controllers
{
    [Route("api/authenticate")]
    public class AuthenticateController : Controller
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticationUser([FromBody]AuthenticationUserCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);

            if (response.HasError)
                return BadRequest(new { errors = response.Errors.Select(x => x) });

            return Ok(new
            {
                response.UserId,
                response.Email,
                response.Token,
                response.Expiration
            });
        }
    }
}