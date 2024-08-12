using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using secondYear.context;
using secondYear.Dto.HotelDTOs;
using secondYear.Models;

namespace secondYear.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> Get()
        {
            var hotel = await _context.Hotels.ToListAsync();

            if (hotel == null)
            {
                return NotFound("No hotel was found");
            }

            var GetHotelDTOs = hotel.Select(h => new GetHotelDTOs
            {
                Id = h.Id,
                Name = h.Name,
                Price = h.Price,
                Image = h.Image,
                Description = h.Description,
                FreeCancellation = h.FreeCancellation,
                ReserveNow = h.ReserveNow
            });

            return Ok(new { message = "The Hotel data is fetched sucessfully", GetHotelDTOs });
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] HotelDto hotelDto)
        {
            try
            {

                var hotel = new Hotel
                {
                    Name = hotelDto.Name,
                    FreeCancellation = hotelDto.FreeCancellation,
                    ReserveNow = hotelDto.ReserveNow,
                    Description = hotelDto.Description,
                    Price = hotelDto.Price,
                    Image = hotelDto.Image
                };

                if (hotel == null)
                {
                    return BadRequest();
                }

                await _context.Hotels.AddAsync(hotel);
                await _context.SaveChangesAsync();

                //  return Ok("Created Successfully");
                return Ok(new { message = "The Hotel created", hotel });


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<Hotel>>> GetById(int id)
        {
            try
            {

               var hotelById = await _context.Hotels.FindAsync(id);
                if (hotelById == null)
                {
                    return NotFound("The given Hotel is Not Found");
                }
                var hotel = new HotelDto
                {
                    Name = hotelById.Name,
                    FreeCancellation = hotelById.FreeCancellation,
                    ReserveNow = hotelById.ReserveNow,
                    Description = hotelById.Description,
                    Price = hotelById.Price,
                    Image = hotelById.Image
                };



                return Ok(new {message = "The Hotel of the given id is :", hotel});
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<IEnumerable<Hotel>>> Delete(int id)
        {
            try
            {

                var findHotel = await _context.Hotels.FindAsync(id);
                if (findHotel == null)
                {
                    return NotFound();
                }

                _context.Hotels.Remove(findHotel);
                await _context.SaveChangesAsync();
                return Ok(new{ message = "The Hotel Has be Pernamently deleted"});
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<IEnumerable<Hotel>>> Update(int id, HotelDto updateHotel)
        {
            try
            {

                var findHotel = await _context.Hotels.FindAsync(id);

                if (findHotel == null)
                {
                    return NotFound();
                }
                findHotel.Name = updateHotel.Name;
                findHotel.Description = updateHotel.Description;
                findHotel.Price = updateHotel.Price;
                findHotel.Image = updateHotel.Image;
                findHotel.FreeCancellation = updateHotel.FreeCancellation;
                findHotel.ReserveNow = updateHotel.ReserveNow;
                await _context.SaveChangesAsync();
                return Ok("Updated Sucessfully");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Hotel>>> SearchByName([FromQuery] string name)
        {
            try
            {

                var hotels = await _context.Hotels.Where(h => h.Name.Contains(name)).ToListAsync();
                

                if (!hotels.Any())
                {
                    return NotFound();
                }

                return Ok(hotels);
            }
            catch
            {
                return BadRequest();
            }


        }

    }
}