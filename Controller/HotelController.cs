using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using secondYear.context;
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

        public IActionResult Create([FromBody]Hotel hotel)
        {

           
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

             return Ok("Created Successfully");
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
    }
}