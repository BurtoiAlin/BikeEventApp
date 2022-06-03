using BikeEvent.Entities;
using BikeEvent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BikeEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private LicentaDatabaseContext _database;
        public LoginController(IConfiguration config, LicentaDatabaseContext database)
        {
            _config = config;
            _database = database;
        }

        [AllowAnonymous]
        [HttpPost]

        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
                {
                    var token = Generate(user);
                    return Ok(token);
                }
                return BadRequest("Parola si/sau emailul sunt incorecte!");
            }

            return NotFound("User not found");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Lastname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserLogin userLogin)
        {
            var currentUser = _database.Users
                .Include(u => u.Role)
                .FirstOrDefault(o => o.Email == userLogin.Email);

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            var emailExist = _database.Users.Any(u => u.Email == userRegister.Email.Trim().ToLower());

            if (emailExist)
            {
                return BadRequest("Email-ul este deja folosit.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);

            var registerUser = new User();

            registerUser.UserId = Guid.NewGuid();
            registerUser.RoleId = Guid.Parse("304b7f2d-e51e-4a5a-b86d-b035ba04afdc");
            registerUser.Name = userRegister.Name;
            registerUser.Lastname = userRegister.Lastname;
            registerUser.Email = userRegister.Email;

            if(userRegister.BirthDate < DateTime.Now)
            {
                registerUser.BirthDate = userRegister.BirthDate;
            }
            else
            {
                return BadRequest("Data de nastere invalida.");
            }
            
            registerUser.GenderId = userRegister.Gender.Trim().ToLower() == "male"
                ? Guid.Parse("661eab79-1fb9-4f7b-b164-2adb35a80188")
                : Guid.Parse("555688fe-c667-4873-b40a-48be24290c1a");
            registerUser.Password = passwordHash;

            _database.Add(registerUser);
            _database.SaveChanges();

            return Ok();
        }

        [HttpPost("addEvent")]
        public async Task<IActionResult> NewEvent(AddEvent addEvent)
        {
            var addedEvent = new Event();

            addedEvent.EventId = Guid.NewGuid();
            addedEvent.EventName = addEvent.EventName;
            addedEvent.EventPic = addEvent.EventPic;
            
            /*validare dataStart si dataFinish
             dataStart trebuie sa fie la minim 7 zile de la data curenta
             dataFinish trebuie sa fie dupa dataStart*/
            if((addEvent.StartDate - DateTime.Now).Days >= 7)
            {
                addedEvent.StartDate = addEvent.StartDate;
                
                if(addEvent.StartDate < addEvent.EndDate)
                {
                    addedEvent.EndDate = addEvent.EndDate;
                }
                else
                {
                    return BadRequest("Data de final trebuie sa fie dupa data de start.");
                }
            }
            else
            {
                return BadRequest("Data de start este prea apropiata.");
            }
             
            addedEvent.EventDescription = addEvent.EventDescription;
            addedEvent.MapPath = addEvent.MapPath;

            _database.Add(addedEvent);
            _database.SaveChanges();

            return Ok();
        }
    }
}
