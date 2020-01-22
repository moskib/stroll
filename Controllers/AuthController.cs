using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stroll.Dtos;
using Stroll.Interfaces;
using Stroll.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Stroll.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository authRepo, IConfiguration config)
        {
            _authRepo = authRepo;
            _config = config;
        }

        [HttpPost("register")] // api/auth/register
        public async Task<ActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        { 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _authRepo.UserExists(userForRegisterDto.Email))
                return BadRequest("Email already exists");

            var userToCreate = new User
            {
                Email = userForRegisterDto.Email,
                Type = userForRegisterDto.Type
            };

            try
            {
                await _authRepo.Register(userToCreate, userForRegisterDto.Password);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                // log exception
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error...");
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userFromRepo =
                await _authRepo.Login(userForLoginDto.Email, userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.UID.ToString()),
                    new Claim(ClaimTypes.Email, userFromRepo.Email)
                }),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }
    }
}
