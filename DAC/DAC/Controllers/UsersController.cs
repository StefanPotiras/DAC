﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAC.Models;
using DAC.Dtos;

namespace DAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Online_Shop2Context _context;
        private readonly ICustomerAuthService _authorization;
        public UsersController(Online_Shop2Context context, ICustomerAuthService authorization)
        {
            _context = context;
            _authorization = authorization;
        }

        //[HttpPost]
        //[Route("register")]
       /* public async Task<ActionResult<bool>> Register([FromBody] RegisterUserDto request)
        {
            if (request == null)
            {
                BadRequest(error: "Request must not be empty!");
            }

            var hashedPassword = _authorization.HashPassword(request.Password);

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = hashedPassword,
                Email = request.Email,
            };

            _unitOfWork.Users.Insert(user);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            return Ok(saveResult);
        }*/

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            User user = new User()
            {
                Username = "hey",
                Password = "234",
                IsAdmin = false,
                IdUser = 1
            };
            _authorization.GetToken(user);

            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
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
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
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

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
