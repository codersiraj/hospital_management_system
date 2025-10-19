using HospitalApplicationAPI.Data;
using HospitalApplicationAPI.Models;
using HospitalApplicationAPI.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        // ✅ GET /api/doctor/{doctorId}/patients
        [HttpGet("{doctorId}/patients")]
        public async Task<IActionResult> GetPatientsByDoctor(string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId))
                return BadRequest(new { Success = false, Message = "DoctorID is required" });

            try
            {
                var doctorIdParam = new SqlParameter("@DoctorID", doctorId);

                var result = await _context.DoctorPatientView
                    .FromSqlRaw("EXEC sp_GetPatientsByDoctor @DoctorID", doctorIdParam)
                    .ToListAsync();

                if (result == null || !result.Any())
                    return NotFound(new { Success = false, Message = "No patients found for this doctor." });

                return Ok(new { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error fetching patients.",
                    Error = ex.Message
                });
            }
        }

    }
}
