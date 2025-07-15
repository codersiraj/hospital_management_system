using HospitalApplicationAPI.Data;
using HospitalApplicationAPI.Models;
using HospitalApplicationAPI.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validRoles = new[] { "Staff", "Doctor", "Lab Assistant", "Admin" };
            if (!validRoles.Contains(request.Role))
                return BadRequest("Invalid role");

            var now = DateTime.Now;
            var plainPassword = now.ToString("ddMMyyyyHHmmss");
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

            var generatedUserId = GenerateUserId(request.FullName);

            var user = new User
            {
                UserId = generatedUserId,
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                Role = request.Role,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy,
                CreatedAt = now,
                Password = hashedPassword
            };

            _context.Users.Add(user);

            // Auto create Doctor profile if role is Doctor
            if (request.Role == "Doctor")
            {
                var doctor = new Doctor
                {
                    UserId = generatedUserId,
                    Specialization = "",
                    Availability = "",
                    Qualification = "",
                    ExperienceYears = 0,
                    Bio = "",
                    CreatedAt = now
                };
                _context.Doctor.Add(doctor);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.UserId,
                user.FullName,
                user.Email,
                user.Role,
                user.IsActive,
                Password = plainPassword
            });
        }

        // READ ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.FullName)
                .ToListAsync();

            return Ok(users);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null) return NotFound("User not found");

            user.FullName = request.FullName;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.Role = request.Role;
            user.IsActive = request.IsActive;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok("User updated successfully");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null) return NotFound("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok("User deleted successfully");
        }

        private string GenerateUserId(string fullName)
        {
            string baseId = fullName.Trim().ToLower().Replace(" ", ".");
            string userId = baseId;
            int suffix = 1;

            while (_context.Users.Any(u => u.UserId == userId))
            {
                userId = $"{baseId}{suffix:D2}";
                suffix++;
            }

            return userId;
        }
    }
}
