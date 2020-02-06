using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.src.Payment.Domain;
using webapi.src.Payment.Domain.Command;
using webapi.src.Shared.Domain.Command;
using webapi.src.Shared.Domain.ValueObject;

namespace webapi.src.Payment.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        public PaymentController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProcessPaymentCommand processPaymentCommand)
        {
            await _commandBus.Send(processPaymentCommand);
            return StatusCode(200);
        }

        [HttpGet]
        public IActionResult Show()
        {
            return StatusCode(200, "hi");
        }
    }
}
