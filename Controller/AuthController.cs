using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondYear.context;
using secondYear.Dto.UserDTOs;

namespace secondYear.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("SignIn")]
         public async Task<IActionResult> UserLogin ([FromBody] LoginDTOs signInDTO)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == signInDTO.UserName);

                if (user == null)
                {
                    return BadRequest("Username does not exist.");
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(signInDTO.Password, user.Password);
                if (!isPasswordValid)
                {
                    return BadRequest("Password is incorrect.");
                }

                return Ok("User signed in successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
    }
}
}