using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC_GMS_API.Contracts;
using POC_GMS_API.Models;
using POC_GMS_API.Shared;
using System;

namespace POC_GMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = JwtTokenHelper.GenerateJwtTokenForLogin(user);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                // create user
                _userService.Create(new Models.DTO.User { 
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserType = "Applicant"
                }, model.Password);
                return Ok("User registration successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
