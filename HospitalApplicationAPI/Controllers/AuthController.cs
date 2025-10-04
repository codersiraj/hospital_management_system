using HospitalApplicationAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using HospitalApplicationAPI.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace HospitalApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == request.UserId);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid User ID or Password" });
            }

            // Simulate token (replace with JWT or session logic as needed)
            var token = Guid.NewGuid().ToString();

            return Ok(new
            {
                message = "Login successful",
                userId = user.UserId,
                fullName = user.FullName,
                role = user.Role,
                token = token
            });
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.Password))
                return BadRequest("UserId and Password are required.");

            var userIdParam = new SqlParameter("@UserId", request.UserId);
            var passwordParam = new SqlParameter("@Password", request.Password);

            var isValidParam = new SqlParameter
            {
                ParameterName = "@IsValid",
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ValidateUser @UserId, @Password, @IsValid OUTPUT",
                userIdParam, passwordParam, isValidParam);

            bool isValid = (bool)isValidParam.Value;

            if (isValid)
                return Ok(new { Success = true, Message = "Login successful" });
            else
                return Unauthorized(new { Success = false, Message = "Invalid credentials" });
        }

    }
}
