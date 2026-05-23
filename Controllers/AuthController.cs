using FullStackApi.Data;
using FullStackApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullStackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(User loginUser)
        {
            var user = _context.Users.FirstOrDefault(x =>

                x.Email == loginUser.Email &&
                x.Password == loginUser.Password
            );

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}