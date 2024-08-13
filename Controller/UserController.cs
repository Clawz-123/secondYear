using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondYear.context;
using secondYear.Dto;
using secondYear.Dto.HotelDTOs;
using secondYear.Dto.UserDTOs;
using secondYear.Models;

namespace secondYear.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {

                var users = await _context.Users.ToListAsync();

                if (users == null)
                {
                    return BadRequest("The user is not found");
                }

                var GetUserDTOs = users.Select(u => new GetUserDTOs
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Password = u.Password,

                });

            return Ok(new { message = "The User data is fetched sucessfully", GetUserDTOs });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PostUserDTOs CreateUserDto)
        {

            var EmailExists =  await _context.Users.SingleOrDefaultAsync(u => u.Email == CreateUserDto.Email);
            if ( EmailExists != null){
                return BadRequest("Email Already Exists");
            }

            var UserNameExists =  await _context.Users.SingleOrDefaultAsync(u => u.UserName == CreateUserDto.UserName);
            if ( UserNameExists != null){
                return BadRequest("UserName Already Exists");
            }

            if(CreateUserDto.Password != CreateUserDto.ConfirmPassword){
                return BadRequest("The Password Does Not Match");
            }

            var HashPassword = BCrypt.Net.BCrypt.HashPassword(CreateUserDto.Password);


            var user = new User
            {
                UserName = CreateUserDto.UserName,
                Email = CreateUserDto.Email,
                Password = HashPassword,
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("created sucessfully");
    }
}
}