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
    public class TravelPackageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TravelPackageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var travel = _context.TravelPackages.ToList();
            return Ok(travel);
        }

        [HttpPost]

        public async Task<ActionResult<IEnumerable<TravelPackage>>> Create([FromBody] TravelPackageDto travelpackageDto)
        {
            try{


            var travelpackage = new TravelPackage
            {
                Name = travelpackageDto.Name,
                Address = travelpackageDto.Address,
                Description = travelpackageDto.Description,
                Price = travelpackageDto.Price,
                Image = travelpackageDto.Image

            };
            _context.TravelPackages.Add(travelpackage);
           await  _context.SaveChangesAsync();
            return Ok("Created Sucessfully");
            }
            catch{
               return BadRequest();
            }

        }
   
        [HttpDelete("{id}")]

        public async Task<ActionResult<IEnumerable<TravelPackage>>> Delete(int id)
        {
            try{

            var travel =await  _context.TravelPackages.FindAsync(id);

            if (travel == null)
            {
                return NotFound();
            }

            _context.TravelPackages.Remove(travel);
            await _context.SaveChangesAsync();
            return Ok("Deleted Sucessfully");
            }
            catch{
                return BadRequest();
            }

        }

        [HttpPut("{id}")]

        public async Task<ActionResult<IEnumerable<TravelPackage>>> Update(int id, TravelPackage updateTravelPackage)
        {
            try{

            var findtravel =await _context.TravelPackages.FindAsync(id);

            if (findtravel == null)
            {
                return NotFound();
            }

            findtravel.Name = updateTravelPackage.Name;
            findtravel.Address = updateTravelPackage.Address;
            findtravel.Price = updateTravelPackage.Price;
            findtravel.Image = updateTravelPackage.Image;
            findtravel.Description = updateTravelPackage.Description;
           await  _context.SaveChangesAsync();
            return Ok("Updated Sucessfully");
        }
        catch{
            return BadRequest();
        }
            }

        [HttpGet("Search")]

        public async Task<ActionResult<IEnumerable<TravelPackage>>> Search([FromQuery] string name)
        {
            try{

            var findtravel =await _context.TravelPackages.Where(g => g.Name.Contains(name)).ToListAsync();

            if (!findtravel.Any())
            {
                return NotFound();
            }

            return Ok(findtravel);
        }
        catch{
            return BadRequest();
        }
            }


    }
}