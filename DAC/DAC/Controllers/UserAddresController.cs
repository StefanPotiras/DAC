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
    public class UserAddresController : ControllerBase
    {
        private readonly ShopContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;
        public UserAddresController(ShopContext context, IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }

        // GET: api/UserAddres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAddres>>> GetUserAddress()
        {
            return await _context.UserAddress.ToListAsync();
        }

        // GET: api/UserAddres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAddres>> GetUserAddres(Guid id)
        {
            var userAddres = await _context.UserAddress.FindAsync(id);

            if (userAddres == null)
            {
                return NotFound();
            }

            return userAddres;
        }

        // PUT: api/UserAddres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAddres(Guid id, UserAddres userAddres)
        {
            if (id != userAddres.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAddres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAddresExists(id))
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

        // POST: api/UserAddres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAddres>> PostUserAddres(UserAddres userAddres)
        {
            _context.UserAddress.Add(userAddres);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAddres", new { id = userAddres.Id }, userAddres);
        }

        // DELETE: api/UserAddres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAddres(Guid id)
        {
            var userAddres = _unitOfWork.UserAdresses.GetById(id);
            if (userAddres == null)
            {
                return NotFound();
            }

            _unitOfWork.UserAdresses.Delete(userAddres);
            var saveResult = await _unitOfWork.SaveChangesAsync();
            return Ok(saveResult);
        }

        private bool UserAddresExists(Guid id)
        {
            return _context.UserAddress.Any(e => e.Id == id);
        }
    }
}
