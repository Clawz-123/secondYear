using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondYear.context;
using secondYear.Dto;
using secondYear.Dto.HotelDTOs;
using secondYear.Dto.UserDTOs;
using secondYear.Models;
using secondYear.service;

namespace secondYear.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailServices _emailServices;

        public UserController(ApplicationDbContext context, EmailServices emailServices)
        {
            _context = context;
            _emailServices= emailServices;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {

                var users = await _context.Users.ToListAsync();

                if (users == null)
                {
                    return BadRequest("The user is not found");
                }

                var GetUserDTOs = users.Select(u => new GetUserDTOs
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Password = u.Password,
                    Role = u.Role

                });

            return Ok(new { message = "The User data is fetched sucessfully", GetUserDTOs });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PostUserDTOs CreateUserDto)
        {

            var EmailExists =  await _context.Users.SingleOrDefaultAsync(u => u.Email == CreateUserDto.Email);
            if ( EmailExists != null){
                return BadRequest("Email Already Exists");
            }

            var UserNameExists =  await _context.Users.SingleOrDefaultAsync(u => u.UserName == CreateUserDto.UserName);
            if ( UserNameExists != null){
                return BadRequest("UserName Already Exists");
            }

            if(CreateUserDto.Password != CreateUserDto.ConfirmPassword){
                return BadRequest("The Password Does Not Match");
            }

            var HashPassword = BCrypt.Net.BCrypt.HashPassword(CreateUserDto.Password);


            var user = new User
            {
                UserName = CreateUserDto.UserName,
                Email = CreateUserDto.Email,
                Password = HashPassword,
                Role = CreateUserDto.Role
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("created sucessfully");
    }

    // [HttpPost("Profile")]

    //     public async Task<ActionResult> Create([FromBody] ProfileDTOs profileDTOs )
    //     {
    //         var profile = new ProfileDTOs
    //         {
    //             ProfileImage = profileDTOs.ProfileImage,
    //             CoverImage = profileDTOs.CoverImage,
    //             Bio = profileDTOs.Bio
    //         };

    //         await _context.Users.AddAsync(profile);
    //         await _context.SaveChangesAsync();
    //         return Ok("Profile crated sucessfully");
    //     }

         [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordDTOs model)
        {
            // Find the user by email (which is used as the Username)
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user == null)
                return BadRequest("User not found.");

            // Simulate generating a password reset token (In reality, you'd generate a secure, unique token)
            var resetToken = Guid.NewGuid().ToString();   // Generate a secure token

            var emailBody = $"This is your Reset Token: {resetToken}";
            await _emailServices.SendEmailAsync(user.Email, "Password Reset", emailBody);

            // Construct reset URL
            // var resetLink = Url.Action("ResetPassword",
            //     "Account",  // Controller name
            //     new { token = resetToken, email = user.Username },  // Query parameters
            //     Request.Scheme);  // Scheme (http or https)

            // var emailBody = $"This is your Reset Token: {resetToken}";

            // // Store the reset token with the user's information in a secure way (e.g., in a database)
            // _context.PasswordResets.Add(new PasswordReset { Token = resetToken });
            // await _context.SaveChangesAsync();

             // Construct the email body
            // var emailBody = $"This is your Reset Token: {resetToken}";
             // Send the email
            // await _emailService.SendEmailAsync(user.Username, "Password Reset", emailBody);

            // Send the reset link via email
            // var emailBody = $"Please reset your password by clicking here: <a href='{resetLink}'>Reset Password</a>";
            // await _emailService.SendEmailAsync(user.Username, "Password Reset", emailBody);

            return Ok("If an account with that email exists, a password reset link has been sent.");
        }


}
}