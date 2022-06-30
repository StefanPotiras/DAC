using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAC;
using DAC.Entities;
using DAC.Repositories;

namespace DAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPaymentsController : ControllerBase
    {
        private readonly ShopContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;
        public UserPaymentsController(ShopContext context, IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }

        // GET: api/UserPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPayment>>> GetUserPayments()
        {
            return await _context.UserPayments.ToListAsync();
        }

        // GET: api/UserPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPayment>> GetUserPayment(Guid id)
        {
            var userPayment = await _context.UserPayments.FindAsync(id);

            if (userPayment == null)
            {
                return NotFound();
            }

            return userPayment;
        }

        // PUT: api/UserPayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPayment(Guid id, UserPayment userPayment)
        {
            if (id != userPayment.Id)
            {
                return BadRequest();
            }

            _context.Entry(userPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserPayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPayment>> PostUserPayment(UserPayment userPayment)
        {
            _context.UserPayments.Add(userPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPayment", new { id = userPayment.Id }, userPayment);
        }

        // DELETE: api/UserPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPayment(Guid id)
        {
            var userPayment = _unitOfWork.Payments.GetById(id);
            if (userPayment == null)
            {
                return NotFound();
            }

            _unitOfWork.Payments.Delete(userPayment);
            var saveResult = await _unitOfWork.SaveChangesAsync();
            return Ok(saveResult);
        }

        private bool UserPaymentExists(Guid id)
        {
            return _context.UserPayments.Any(e => e.Id == id);
        }
    }
}
