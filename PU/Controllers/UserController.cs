using Microsoft.AspNetCore.Mvc;
using PU.Models.Entities;
using PU.Services.Interfaces;

namespace PU.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IBaseService<User> _userService;

        public UserController(IBaseService<User> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allUser = await _userService.GetAllAsync();
            return Ok(allUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var user = await _userService.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(User);
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var created = await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.UserId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.UserId) return BadRequest();
            await _userService.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }


    }
}