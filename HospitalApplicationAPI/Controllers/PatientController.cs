using HospitalApplicationAPI.Data;
using HospitalApplicationAPI.Models;
using HospitalApplicationAPI.Models.Request;
using HospitalApplicationAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Step 1: Generate Unique UserId String from FullName
            var baseId = request.PatientName.Trim().ToLower().Replace(" ", ".");
            var similarUsers = _context.Users
                .Where(u => u.FullName.ToLower() == request.PatientName.ToLower())
                .ToList();

            string generatedUserId = baseId;
            if (similarUsers.Count > 0)
            {
                int suffix = similarUsers.Count + 1;
                generatedUserId = $"{baseId}{suffix}";
            }

            // Step 2: Generate password and hash it
            string rawPassword = $"Pwd{DateTime.Now:yyyyMMddHHmmss}";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(rawPassword);

            // Step 3: Create new patient with fallback defaults
            var newPatient = new Patient
            {
                PatientName = request.PatientName,
                BloodGroup = string.IsNullOrWhiteSpace(request.BloodGroup) ? "-" : request.BloodGroup,
                DOB = request.DOB == null ? DateTime.Today : request.DOB, // fallback to today's date if null
                ICType = string.IsNullOrWhiteSpace(request.ICType) ? "NRIC" : request.ICType,
                NRIC = request.NRIC, // This should always be required
                Nationality = string.IsNullOrWhiteSpace(request.Nationality) ? "Malaysia" : request.Nationality,
                Religion = string.IsNullOrWhiteSpace(request.Religion) ? "Islam" : request.Religion,
                Race = string.IsNullOrWhiteSpace(request.Race) ? "Indian Muslim" : request.Race,
                Language = string.IsNullOrWhiteSpace(request.Language) ? "English" : request.Language,
                Address1 = string.IsNullOrWhiteSpace(request.Address1) ? "-" : request.Address1,
                Address2 = string.IsNullOrWhiteSpace(request.Address2) ? "-" : request.Address2,
                Address3 = string.IsNullOrWhiteSpace(request.Address3) ? "-" : request.Address3,
                State = string.IsNullOrWhiteSpace(request.State) ? "Selangor" : request.State,
                Country = string.IsNullOrWhiteSpace(request.Country) ? "Malaysia" : request.Country,
                Pincode = string.IsNullOrWhiteSpace(request.Pincode) ? "-" : request.Pincode,
                Phone1 = request.Phone1 ?? "0",
                Phone2 = request.Phone2 ?? "0",
                Email = string.IsNullOrWhiteSpace(request.Email) ? "-" : request.Email,

                RelativeID1 = string.IsNullOrWhiteSpace(request.RelativeID1) ? "-" : request.RelativeID1,
                RelativeName1 = string.IsNullOrWhiteSpace(request.RelativeName1) ? "-" : request.RelativeName1,
                Relationship1 = string.IsNullOrWhiteSpace(request.Relationship1) ? "-" : request.Relationship1,
                RelativeID2 = string.IsNullOrWhiteSpace(request.RelativeID2) ? "-" : request.RelativeID2,
                RelativeName2 = string.IsNullOrWhiteSpace(request.RelativeName2) ? "-" : request.RelativeName2,
                Relationship2 = string.IsNullOrWhiteSpace(request.Relationship2) ? "-" : request.Relationship2,

                UserID = generatedUserId,
                Password = hashedPassword
            };


            _context.Patient.Add(newPatient);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Patient created successfully",
                patientId = newPatient.PatientId,
                userId = newPatient.UserID,
                tempPassword = rawPassword  // 🚨 Only for testing — remove this in production
            });
        }

        [HttpGet("getallpatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _context.Patient
                .Select(p => new PatientResponse
                    {
                        PatientName = p.PatientName,
                        NRIC = p.NRIC,
                        DOB = p.DOB,
                        Phone1 = p.Phone1,
                        Email = p.Email,
                        BloodGroup = p.BloodGroup,
                        ICType = p.ICType,
                        Nationality = p.Nationality,
                    })
                    .ToListAsync(); 
            return Ok(patients);
        }

        [HttpGet("check-nric")]
        public IActionResult CheckNRICExists([FromQuery] string nric)
        {
            if (string.IsNullOrWhiteSpace(nric))
                return BadRequest("NRIC is required.");

            bool exists = _context.Patient.Any(p => p.NRIC == nric);

            return Ok(new { exists });
        }

        [HttpGet("get-by-nric")]
        public IActionResult GetByNRIC([FromQuery] string nric)
        {
            var patient = _context.Patient
                .Select(p => new PatientResponse
                {
                    PatientName = p.PatientName,
                    NRIC = p.NRIC,
                    DOB = p.DOB,
                    Phone1 = p.Phone1,
                    Email = p.Email,
                    BloodGroup = p.BloodGroup,
                    ICType = p.ICType,
                    Nationality = p.Nationality,
                })
                .FirstOrDefault(p => p.NRIC == nric);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

    }
}
