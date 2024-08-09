using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<Hotel>>> Get()
        {
            var hotel = await _context.Hotels.ToListAsync();
            return Ok(hotel);
        }

         [HttpPost]

        public async Task<ActionResult<IEnumerable<Hotel>>> Create([FromBody]Hotel hotel)
        {
            try{

            //        var hotel = new Hotel
            // {
            //     Name = hotelDto.Name,
            //     Address = hotelDto.Address,
            //     Description = hotelDto.Description,
            //     Price = hotelDto.Price,
            //     Image = hotelDto.Image
            // };

            if(hotel == null){
                return BadRequest();
            }
           
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();

             return Ok("Created Successfully");

            }catch(Exception e) {
                return BadRequest(e);
            }


         
        }

      

      [HttpGet("{id}")]

      public async Task <ActionResult<IEnumerable<Hotel>>> GetById(int id)
      {
        try{

        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel == null){
            return NotFound();
        }

        return Ok(hotel);
      }
      catch{
        return BadRequest();      
        }
        }


        [HttpDelete("{id}")]

        public async Task <ActionResult<IEnumerable<Hotel>>> Delete(int id)
        {
            try{

            var findHotel = await _context.Hotels.FindAsync(id);


            if (findHotel == null){
                return NotFound();
            } 

            _context.Hotels.Remove(findHotel);
            await _context.SaveChangesAsync();
            return Ok("Removed Sucessfully");
            }
            catch{
               return BadRequest();
            }
        }

        [HttpPut("{id}")]

        public async Task <ActionResult<IEnumerable<Hotel>>> Update(int id, Hotel updateHotel)
        {
            try{

            var findHotel =await _context.Hotels.FindAsync(id);

            if(findHotel == null){
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
            catch{
              return  BadRequest();
            }
        }

        [HttpGet("Search")]
        public async Task <ActionResult<IEnumerable<Hotel>>> SearchByName([FromQuery] string name)
        {
            try{

            var hotels = await _context.Hotels.Where(h => h.Name.Contains(name)).ToListAsync();

            if(!hotels.Any()){
                return NotFound();
            }

            return Ok(hotels);
            }
            catch{
               return BadRequest();
            }


        }
        
    }
}