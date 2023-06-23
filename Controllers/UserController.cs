using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoolApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CoolApi.Controllers

{
    [Route("[controller]")]
    [ApiController]

    public class UserController : Controller
    {

        private readonly UserContext _cxt;

        public UserController(UserContext context)
        {
            _cxt = context;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult AuthenticateUser(User user)
        {
            var query = $"SELECT * FROM Users WHERE email = '{user.email}' AND password = '{user.password}'";

            var result = _cxt.Users.FromSqlRaw(query).FirstOrDefault();

            if (result == null)
            {
                return BadRequest("Invalid email or password");
            }

            // return Ok(user);
            var token = GenerateJwtToken(result);

            // Return the token as a response
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ABCDeujujsik@!!AshsnskajuhABCDeujujsik@!!AshsnskajuhABCDeujujsik@!!Ashsnskajuh"); // Increase the key size to 384 bits (48 bytes)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Email, user.email),
            new Claim("Password", user.password),
            new Claim("id", user.id.ToString()),
            new Claim("active", user.active.ToString()),
            new Claim("role", user.role),

            // Add additional claims as needed
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword.Equals(storedPassword);
        }



        [HttpPost]
        [Route("AddUsers")]
        [Authorize]

        public IActionResult CreateStudent(User user)
        {
            _cxt.Users.Add(user);
            _cxt.SaveChanges();

            return Ok();
        }

    }
}