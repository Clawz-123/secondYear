using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using secondYear.context;
using secondYear.Dto;
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
        public IActionResult Get()
        {
            var hotel = _context.Hotels.ToList();
            return Ok(hotel);
        }

         [HttpPost]

        public IActionResult Create([FromBody]HotelDto hotelDto)
        {

            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Address = hotelDto.Address,
                Description = hotelDto.Description,
                Price = hotelDto.Price,
                Image = hotelDto.Image
            };
           
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

             return Ok("Created Successfully");
        }

      

      [HttpGet("{id}")]

      public IActionResult GetById(int id)
      {
        var hotel = _context.Hotels.Find(id);

        if (hotel == null){
            return NotFound();
        }

        return Ok(hotel);
      }


        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var findHotel = _context.Hotels.Find(id);


            if (findHotel == null){
                return NotFound();
            } 

            _context.Hotels.Remove(findHotel);
            _context.SaveChanges();
            return Ok("Removed Sucessfully");

        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, Hotel updateHotel)
        {
            var findHotel = _context.Hotels.Find(id);

            if(findHotel == null){
                return NotFound();            
                }

                findHotel.Name = updateHotel.Name;
                findHotel.Address = updateHotel.Address;
                findHotel.Description = updateHotel.Description;
                findHotel.Price = updateHotel.Price;
                findHotel.Image = updateHotel.Image;
                _context.SaveChanges();
                return Ok("Updated Sucessfully");
        }

        [HttpGet("Search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            var hotels = _context.Hotels.Where(h => h.Name.Contains(name)).ToList();

            if(!hotels.Any()){
                return NotFound();
            }

            return Ok(hotels);


        }
        
    }
}