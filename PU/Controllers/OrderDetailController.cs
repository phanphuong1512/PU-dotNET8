using Microsoft.AspNetCore.Mvc;
using PU.Models.Entities;
using PU.Services.Interfaces;

namespace PU.Controllers
{
    [ApiController]
    [Route("api/orderdetails")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IBaseService<OrderDetail> _orderDetailService;
        private readonly IBaseService<Order> _orderService;
        private readonly IBaseService<Product> _productService;

        public OrderDetailController(
            IBaseService<OrderDetail> orderDetailService,
            IBaseService<Order> orderService,
            IBaseService<Product> productService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _productService = productService;
        }

        // GET: api/orderdetails
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var details = await _orderDetailService.GetAllAsync();
            return Ok(details);
        }

        // GET: api/orderdetails/{orderId}/{productId}
        [HttpGet("{orderId}/{productId}")]
        public async Task<IActionResult> GetByIds(int orderId, int productId)
        {
            var all = await _orderDetailService.GetAllAsync();
            var detail = all.FirstOrDefault(d => d.OrderId == orderId && d.ProductId == productId);

            if (detail == null)
                return NotFound();

            return Ok(detail);
        }

        // GET: api/orderdetails/order/{orderId}
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrder(int orderId)
        {
            var all = await _orderDetailService.GetAllAsync();
            var details = all.Where(d => d.OrderId == orderId);
            return Ok(details);
        }

        // POST: api/orderdetails
        [HttpPost]
        public async Task<IActionResult> Create(OrderDetail orderDetail)
        {
            // Validate Order and Product existence
            var order = await _orderService.GetByIdAsync(orderDetail.OrderId);
            if (order == null)
                return BadRequest($"Order {orderDetail.OrderId} does not exist.");

            var product = await _productService.GetByIdAsync(orderDetail.ProductId);
            if (product == null)
                return BadRequest($"Product {orderDetail.ProductId} does not exist.");

            var created = await _orderDetailService.AddAsync(orderDetail);
            return CreatedAtAction(
                nameof(GetByIds),
                new { orderId = created.OrderId, productId = created.ProductId },
                created);
        }

        // PUT: api/orderdetails/{orderId}/{productId}
        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> Update(int orderId, int productId, OrderDetail orderDetail)
        {
            if (orderId != orderDetail.OrderId || productId != orderDetail.ProductId)
                return BadRequest("Mismatched OrderId or ProductId.");

            var all = await _orderDetailService.GetAllAsync();
            var existing = all.FirstOrDefault(d => d.OrderId == orderId && d.ProductId == productId);
            if (existing == null)
                return NotFound();

            await _orderDetailService.UpdateAsync(orderDetail);
            return NoContent();
        }

        // DELETE: api/orderdetails/{orderId}/{productId}
        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> Delete(int orderId, int productId)
        {
            var all = await _orderDetailService.GetAllAsync();
            var existing = all.FirstOrDefault(d => d.OrderId == orderId && d.ProductId == productId);
            if (existing == null)
                return NotFound();

            await _orderDetailService.DeleteAsync(orderId); // requires service update
            return NoContent();
        }
    }
}
