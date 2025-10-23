using Microsoft.AspNetCore.Mvc;
using PU.Models.Entities;
using PU.Services.Interfaces;

namespace PU.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class ReviewController : ControllerBase
    {
        private readonly IBaseService<Review> _reviewService;

        public ReviewController(IBaseService<Review> reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allReview = await _reviewService.GetAllAsync();
            return Ok(allReview);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var review = await _reviewService.GetByIdAsync(id);
            return review == null ? NotFound() : Ok(review);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            var created = await _reviewService.AddAsync(review);
            return CreatedAtAction(nameof(GetById), new { id = created.ReviewId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Review review)
        {
            if (id != review.ReviewId) return BadRequest();
            await _reviewService.UpdateAsync(review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reviewService.DeleteAsync(id);
            return NoContent();
        }


    }
}