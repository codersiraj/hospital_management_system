using HospitalApplicationAPI.Data;
using HospitalApplicationAPI.Models;
using HospitalApplicationAPI.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoctorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            try
            {
                // Execute the stored procedure
                var doctors = await _context.Set<GetDoctorsRequest>()
                    .FromSqlRaw("EXEC spGetDoctors")
                    .ToListAsync();

                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching doctors.", error = ex.Message });
            }
        }

    }
}
