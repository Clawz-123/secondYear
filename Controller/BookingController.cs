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
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

    [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> Get()
        {
            var booking = await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Hotel)
            .ToListAsync();
            return Ok(booking);
            // .Include(r) 
        }

        [HttpPost]

        public async Task<ActionResult<IEnumerable<Booking>>> Create([FromBody] BookingDto bookingDto)
        {
            var booking = new Booking{
                 DateTime =  bookingDto.DateTime,
                UserId = bookingDto.UserId,
                // HotelId = bookingDto.HotelId,
            };
           await  _context.Bookings.AddAsync(booking);
           await _context.SaveChangesAsync();
           return Ok("Created Sucessfuly");
        }




    }
}