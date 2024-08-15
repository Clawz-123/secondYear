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

                // Store token and user data in the session
                    HttpContext.Session.SetString("AuthToken", token);
                    HttpContext.Session.SetString("Id", user.Id.ToString());
                    HttpContext.Session.SetString("UserName", user.UserName ?? string.Empty);
                    HttpContext.Session.SetString("UserRole", user.Role ?? string.Empty);

                return Ok(new { Message = "User signed in successfully!", Token = token });

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