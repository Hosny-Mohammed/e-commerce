using e_commerce.DTOs;
using e_commerce.Repositories.Category_Repository;
using e_commerce.Repositories.User_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repo;
        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTOGet>>> GetAll()
        {
            var categories = await _repo.GetAll();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTOGet>> GetAll(int id)
        {
            var categories = await _repo.GetById(id);
            if (categories == null)
                return NotFound();
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryDTOPost dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _repo.Add(dto);
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CategoryDTOPost dto, int id)
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
