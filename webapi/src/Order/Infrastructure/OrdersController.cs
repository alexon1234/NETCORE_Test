using Microsoft.AspNetCore.Mvc;

namespace webapi.src.Payment.Infrastructure
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Show()
        {
            return StatusCode(200, "h");
        }
    }

}