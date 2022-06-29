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
using DAC.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace DAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : WebApiController
    {
        private readonly ShopContext _context;

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;

        public UsersController(ShopContext context, IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }
        [HttpPut]
        [Route("my-account")]
       
        public ActionResult<bool> AddProducts(Guid product)
        {
            var productFull = _unitOfWork.Products.GetProductBy(product);
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var user = _unitOfWork.Users.GetUserById((Guid)userId);
         
            user.Cart.Products.Add(productFull);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<bool>> Register([FromBody] RegisterUserDto request)
        {
            if (request == null)
            {
                BadRequest(error: "Request must not be empty!");
            }
            var user1 = _unitOfWork.Users.GetUserByUsername(request.Username);
            if (user1 != null) return BadRequest("Change Username");
            var hashedPassword = _authorization.HashPassword(request.Password);

            var user = new User()
            {         
               Password= hashedPassword,
               Username=request.Username,
               IsAdmin=false
            };

            _unitOfWork.Users.Insert(user);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            return Ok(saveResult);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<ResponseLogin> Login([FromBody] RequestLogin request)
        {
            var user = _unitOfWork.Users.GetUserByUsername(request.Uername);
            if (user == null) return BadRequest("User not found!");

            var samePassword = _authorization.VerifyHashedPassword(user.Password, request.Password);
            if (!samePassword) return BadRequest("Invalid password!");

            var user_jsonWebToken = _authorization.GetToken(user);

            return Ok(new ResponseLogin
            {
                Token = user_jsonWebToken
            });
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
           
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
