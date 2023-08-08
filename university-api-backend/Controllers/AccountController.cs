using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using university_api_backend.DataAccess;
using university_api_backend.Helpers;
using university_api_backend.Models.DataModels;

namespace university_api_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UniversityDbContext _context;

        public AccountController(JwtSettings jwtSettings, UniversityDbContext context)
        {
            _jwtSettings = jwtSettings;
            _context = context;
        }


        //TODO: change by real users

        private IEnumerable<Users> Logins = new List<Users>()
        {
            new Users()
            {
                Id = 1,
                Email = "wilden@gmail.com",
                Name = "Admin",
                Password = "Admin"
            },
            new Users()
            {
                Id = 2,
                Email = "peep@gmail.com",
                Name = "User1",
                Password = "pepe"
            }
        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var token = new UserTokens();

                var searchUser = (from user in _context.Users
                                  where user.Name == userLogin.Username
                                  && user.Password == userLogin.Password
                                  select user).FirstOrDefault();
                Console.WriteLine("User " + searchUser);


                var valid = Logins.Any(user => user.Name.Equals(userLogin.Username, StringComparison.OrdinalIgnoreCase));

                if(searchUser != null)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.Username, StringComparison.OrdinalIgnoreCase));

                    token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings); ;

                    
                }
                else
                {
                    return BadRequest("Wrong credentials");
                }
                return Ok(token);

        }
            catch (Exception ex)
            {
                throw new Exception("Get token error" + ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }

    
}
