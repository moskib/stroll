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
using Stroll.Services;
using Stroll.Data;
using Stroll.Enums;

namespace Stroll.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _repo;
        private readonly IConfiguration _config;

        public AuthController(ApplicationDbContext context, IConfiguration config)
        {
            _repo = new UnitOfWork(context);
            _config = config;
        }

        [HttpPost("register")] // api/auth/register
        public async Task<ActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _repo.Auth.UserExists(userForRegisterDto.Email))
                return BadRequest("Email already exists");

            var userToCreate = new User
            {
                UID = Guid.NewGuid(),
                Email = userForRegisterDto.Email,
                Type = userForRegisterDto.Type
            };

            try
            {
                await _repo.Auth.Register(userToCreate, userForRegisterDto.Password);
                if ((UserType)userForRegisterDto.Type == UserType.Client)
                {
                    await _repo.ClientUser.CreateClientUser(userForRegisterDto, userToCreate.UID);
                }
                else
                {
                    await _repo.BusinessUser.CreateBusinessUser(userForRegisterDto, userToCreate.UID);
                }
                await _repo.SaveChangesAsync();
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
                await _repo.Auth.Login(userForLoginDto.Email, userForLoginDto.Password);

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
