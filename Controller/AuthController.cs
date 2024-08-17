using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondYear.context;
using secondYear.Dto.UserDTOs;
using secondYear.service;

namespace secondYear.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
           private readonly TokenServices _tokenService;

        public AuthController(ApplicationDbContext context, TokenServices tokenServices)
        {
            _context = context;
            _tokenService = tokenServices;
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
                  // Generate JWT token
                var token = _tokenService.GenerateToken(user);

                var response = new {
                    Token = token,
                    Id = user.Id,
                    Role =  user.Role,
                    ExpiresIn = DateTime.Now.AddMinutes(30)
                };

                return Ok(new { Message = "User signed in successfully!", response = response });

                // return Ok("User signed in successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

    }

    [Authorize]
    [HttpGet("verify-token")]
    public IActionResult VerifyToken(){
        return Ok("User Authorized");

    }
}
}