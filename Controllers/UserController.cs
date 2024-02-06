using userAuth.Models;
using userAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace userAuth.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase{
        private readonly UserService _userService;
        
        public UserController(UserService userService) => _userService = userService;

        [HttpGet]
        public async Task<List<User>> Get() => await _userService.GetAsync();

        [HttpGet("{id:length(50)}")]
        public async Task<ActionResult<User>> Get(string id){
            var user = await _userService.GetAsync(id);

            if (user is null){
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User newUser){
            await _userService.CreateAsync(newUser);
            return CreatedAtAction(nameof(Get), new { id = newUser.id }, newUser);
        }

        [HttpPut("{id:length(50)}")]
        public async Task<IActionResult> Update(string id, User updatedUser){
            var _user = await _userService.GetAsync(id);

            if (_user is null){
                return NotFound();
            }

            updatedUser.id = _user.id;
            await _userService.UpdateAsync(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(50)}")]
        public async Task<IActionResult> Delete(string id){
            var user = await _userService.GetAsync();

            if (user is null){
                return NotFound();
            }

            await _userService.RemoveAsync(id);
            return NoContent();
        }
    }
}
