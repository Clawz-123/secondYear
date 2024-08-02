using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using secondYear.context;
using secondYear.Dto;
using secondYear.Models;

namespace secondYear.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult Get()
        {
            var reviews = _context.Reviews.ToList();
            return Ok(reviews);

        }

        [HttpPost]

        public IActionResult Create([FromBody] ReviewDto reviewDto)
        {

            var review = new Review
            {
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                HotelId = reviewDto.HotelId
            };
           
           _context.Reviews.Add(review);
            _context.SaveChanges();

            return Ok("Created Sucessfully");

        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var findReviews = _context.Reviews.Find(id);

            if(findReviews == null){
                return NotFound();
            }  

            _context.Reviews.Remove(findReviews);
            _context.SaveChanges();
            return Ok("Deleted Sucessfully");

        }

    }
}