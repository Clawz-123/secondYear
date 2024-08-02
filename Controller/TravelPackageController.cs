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

        public IActionResult Create([FromBody] TravelPackage travelpackage)
        {
            _context.TravelPackages.Add(travelpackage);
            _context.SaveChanges();
            return Ok("Created Sucessfully");

        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var travel = _context.TravelPackages.Find(id);

            if (travel == null)
            {
                return NotFound();
            }

            _context.TravelPackages.Remove(travel);
            _context.SaveChanges();
            return Ok("Deleted Sucessfully");

        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, TravelPackage updateTravelPackage)
        {
            var findtravel = _context.TravelPackages.Find(id);

            if (findtravel == null)
            {
                return NotFound();
            }

            findtravel.Name = updateTravelPackage.Name;
            findtravel.Address = updateTravelPackage.Address;
            findtravel.Price = updateTravelPackage.Price;
            findtravel.Image = updateTravelPackage.Image;
            findtravel.Description = updateTravelPackage.Description;
            _context.SaveChanges();
            return Ok("Updated Sucessfully");
        }

        [HttpGet("Search")]

        public IActionResult Search([FromQuery] string name)
        {
            var findtravel = _context.TravelPackages.Where(g => g.Name.Contains(name)).ToList();

            if (!findtravel.Any())
            {
                return NotFound();
            }

            return Ok(findtravel);
        }


    }
}