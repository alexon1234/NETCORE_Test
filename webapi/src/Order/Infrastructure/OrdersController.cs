using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.src.Order.Application;
using webapi.src.Order.Domain;
using webapi.src.Shared.Domain;

namespace webapi.src.Payment.Infrastructure
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public OrdersController(ICommandBus command, IQueryBus queryBus)
        {
            _commandBus = command;
            _queryBus = queryBus;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _queryBus.Send<FindOrderByIdQuery, Order.Domain.Order>(
                new FindOrderByIdQuery()
                {
                    Id = id
                }
            ));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            await _commandBus.Send(command);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}/delivered")]
        public async Task<IActionResult> Delivered(Guid id)
        {
            await _commandBus.Send(new OrderDeliveredCommand()
            {
                Id = id,
            });
            return Ok();
        }

        [HttpPatch]
        [Route("{id}/shipped")]
        public async Task<IActionResult> Shipped(Guid id)
        {
            await _commandBus.Send(new OrderShippedCommand()
            {
                Id = id,
            });
            return Ok();
        }
    }

}