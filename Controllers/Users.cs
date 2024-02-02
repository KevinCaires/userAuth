using userAuth.Models;
using userAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace userAuth.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase{
        private readonly UserService _userService;
        
        public UserController(UserService userService) => this._userService = userService;

        [HttpGet]
        public async Task<List<Users>> Get() => await this._userService.GetAsync();

        [HttpGet("{id:length(50)}")]
        public async Task<ActionResult<Users>> Get(string id){
            var user = await this._userService.GetAsync(id);

            if (user is null){
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Users user){
            await this._userService.CreateAsync(user);
            return CreatedAtAction(nameof(Get), new {id = user.id}, user);
        }

        [HttpPut("{id:length(50)}")]
        public async Task<IActionResult> Update(string id, Users user){
            var _user = await this._userService.GetAsync(id);

            if (_user is null){
                return NotFound();
            }

            user.id = _user.id;
            await this._userService.UpdateAsync(user.id, user);

            return NoContent();
        }

        [HttpDelete("{id:length(50)}")]
        public async Task<IActionResult> Delete(string id){
            var user = await this._userService.GetAsync();

            if (user is null){
                return NotFound();
            }

            await this._userService.RemoveAsync(id);
            return NoContent();
        }
    }
}
