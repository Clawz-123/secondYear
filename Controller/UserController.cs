using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondYear.context;
using secondYear.Dto;
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
            try{

            var users = await _context.Users.ToListAsync() ;
            return Ok(users);
            }
            catch{
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]  UserDto userDto)
        {
            var user = new User{
                Name = userDto.Name,
                Role = userDto.Role,
                Email = userDto.Email,
                Password = userDto.Password,
                Image = userDto.Image,
                CoverImage = userDto.CoverImage,
                Biodata = userDto.Biodata
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("created sucessfully");

        }
    }
}