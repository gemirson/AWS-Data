using System.Linq;
using System.Threading.Tasks;
using AWS.Data.AppDynamo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AWS.Data.AppDynamo.Controllers
{
    
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrderController(ILogger<OrderController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Read all messages
        /// </summary>
        /// <remarks>
        [HttpGet("Pedidos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetMessagesAsync()
        {
            var result = await _orderRepository.GetAllAsync();

            if (!result.Any())
            {
                return Ok(result);
            }
            _logger.LogError(default(EventId), $"Found fails to {nameof(OrderController)} in GetMessagesAsync");
            return BadRequest("Error read all messages");

        }

    }
}
