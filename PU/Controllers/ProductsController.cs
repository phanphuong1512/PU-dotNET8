using Microsoft.AspNetCore.Mvc;
using PU.Models.Entities;
using PU.Services.Interfaces;

namespace PU.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IBaseService<Product> _service;

        public ProductsController(IBaseService<Product> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var created = await _service.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.ProductId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.ProductId) return BadRequest();
            await _service.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}