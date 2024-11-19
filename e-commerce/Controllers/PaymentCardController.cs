using e_commerce.DTOs;
using e_commerce.Repositories.PaymentCard_Repository;
using e_commerce.Repositories.Product_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCardController : ControllerBase
    {
        private readonly IPaymentCardRepository _repo;
        public PaymentCardController(IPaymentCardRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentCardDTOGet>>> GetAll()
        {
            var paymentCards = await _repo.GetAll();
            return Ok(paymentCards);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentCardDTOGet>> GetAll(int id)
        {
            var paymentCards = await _repo.GetById(id);
            if (paymentCards == null)
                return NotFound();
            return Ok(paymentCards);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PaymentCardDTOPost dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _repo.Add(dto);
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] PaymentCardDTOPost dto, int id)
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
            if(!exist) return NotFound();
            return Ok();
        }
    }
}
