using e_commerce.DTOs;
using e_commerce.Repositories.User_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTOGet>>> GetAll() 
        { 
            var users = await _repo.GetAll();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTOGet>> GetAll(int id)
        {
            var users = await _repo.GetById(id);
            if (users == null)
                return NotFound();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserDTOPost dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            bool user = await _repo.Add(dto);
            if(!user)
                return BadRequest("Username is already used");
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserDTOPost dto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool user = await _repo.Update(dto, id);
            if (!user)
                return NotFound();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool exist = await _repo.Delete(id);
            if (!exist) return NotFound();
            return Ok();
        }
    }
}
