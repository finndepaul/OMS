using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Application.DataTransferObjs.Order;
using OMS.Application.Interfaces.Repositories.ReadOnly;
using OMS.Application.Interfaces.Repositories.ReadWrite;
using OMS.Application.ValueObjs.Paginations;

namespace OMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderReadWriteRepository _orderReadWriteRepository;
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        public OrdersController(IOrderReadWriteRepository orderReadWriteRepository, IOrderReadOnlyRepository orderReadOnlyRepository)
        {
            _orderReadWriteRepository = orderReadWriteRepository;
            _orderReadOnlyRepository = orderReadOnlyRepository;
        }
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult<OrderDto>> GetAllOrdersAsync([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] PagingParameters pagingParameters, CancellationToken cancellationToken)
        {
            var response = await _orderReadOnlyRepository.GetAllOrderAsync(fromDate, toDate, pagingParameters, cancellationToken);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetOrderById/{orderId}")]
        public async Task<ActionResult<OrderDetailDto>> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var response = await _orderReadOnlyRepository.GetOrderByIdAsync(orderId, cancellationToken);
            return Ok(response);
        }
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<ActionResult> CreateOrderAsync([FromBody] OrderCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _orderReadWriteRepository.CreateOrderAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
