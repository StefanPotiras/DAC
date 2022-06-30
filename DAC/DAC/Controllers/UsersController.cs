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
            var cart = new Cart();
            
            var user = new User()
            {
                Password = hashedPassword,
                Username = request.Username,
                IsAdmin = false,
              
            };
            cart.User = user;
     
            _unitOfWork.Users.Insert(user);
            _unitOfWork.Carts.Insert(cart);
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
        [Authorize(Roles = "Admin")]
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
        [HttpPut]
       // [Authorize(Roles = "User")]
        [Route("changePassword")]
        public async Task<ActionResult<bool>> PutUser([FromBody]ChangePassDtos password)
        {
            var userId = GetUserId();
            if (userId ==null)
            {
                return BadRequest();
            }
            var user = _unitOfWork.Users.GetUserById(userId); 
            var hashedPassword = _authorization.HashPassword(password.Password);
            user.Password = hashedPassword;
            _unitOfWork.Users.Update(user);

            var saveResult = await _unitOfWork.SaveChangesAsync();

            return Ok(saveResult);
        }

      
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var users = _unitOfWork.Users.GetById(id);
            if (users == null)
            {
                return NotFound();
            }

            _unitOfWork.Users.Delete(users);
            var cart=_unitOfWork.Carts.GetCartBy(id);
            _unitOfWork.Carts.Delete(cart);
            _unitOfWork.Orders.DeleteByID(id);
                     
            var saveResult = await _unitOfWork.SaveChangesAsync();
            return Ok(saveResult);
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
