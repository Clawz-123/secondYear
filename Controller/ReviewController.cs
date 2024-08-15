using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task <ActionResult<IEnumerable<Review>>> Get()
        {
            try{
            var reviews =await  _context.Reviews.ToListAsync();

            var ReviewDto = reviews.Select(h => new ReviewDto{
                Rating = h.Rating,
                Comment = h.Comment,
                HotelId = h.HotelId,
                // UserId = h.UserId,
                DateTime = h.DateTime
            });
            return Ok(reviews);

            }
            catch{
                return BadRequest();
            }

        }

        [HttpPost]

        public async Task <ActionResult<IEnumerable<Review>>> Create([FromBody] ReviewDto reviewDto)
        {
            try{

            var review = new Review
            {
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                HotelId = reviewDto.HotelId,
                UserId = reviewDto.UserId,
                DateTime = reviewDto.DateTime
            };
           
           _context.Reviews.Add(review);
           await _context.SaveChangesAsync();

            return Ok("Created Sucessfully");
            }
            catch{
               return  BadRequest();
            }


        }

        [HttpDelete("{id}")]

        public async Task <ActionResult<IEnumerable<Review>>> Delete(int id)
        {
            try{

            var findReviews =await  _context.Reviews.FindAsync(id);

            if(findReviews == null){
                return NotFound();
            }  

            _context.Reviews.Remove(findReviews);
            await _context.SaveChangesAsync();
            return Ok("Deleted Sucessfully");
            }
            catch{
                return BadRequest();
            }

        }

    }
}