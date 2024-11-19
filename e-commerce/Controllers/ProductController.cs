using e_commerce.DTOs;
using e_commerce.Repositories.Product_Repository;
using e_commerce.Repositories.User_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTOGet>>> GetAll()
        {
            var products = await _repo.GetAll();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTOGet>> GetAll(int id)
        {
            var products = await _repo.GetById(id);
            if (products == null)
                return NotFound();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDTOPost dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _repo.Add(dto);
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ProductDTOPost dto, int id)
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
