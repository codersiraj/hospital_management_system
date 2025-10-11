using HospitalApplicationAPI.Data;
using HospitalApplicationAPI.Models;
using HospitalApplicationAPI.Models.Request;
using HospitalApplicationAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        public async Task<IActionResult> CreatePatient([FromBody] MemberRequest request, [FromQuery] string doctorId)
        {
            try
            {
                var isExistParam = new SqlParameter
                {
                    ParameterName = "@isExist",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                // ✅ Call stored procedure with EF Core
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.spInsertMemberNew " +
                    "@PhotoUrl, @MemberType, @MemberSubType, @IDType, @NRIC, @IDProof1, @IDProof2, " +
                    "@FullName1, @FullName2, @RelativeID1, @RelativeID2, @RelativeName1, @RelativeName2, @Relationship1, @Relationship2, " +
                    "@DOB, @Gender, @MStatus, @Nationality, @Address1, @Address2, @Address3, @Address4, @EAddress1, @EAddress2, @EAddress3, " +
                    "@PostCode, @District, @StateName, @Country, @Email, @PH1, @PH2, @Blood, @IsPWD, @Remark, " +
                    "@DrSpecialization, @DrDegree, @DrCertificate, @DrisOnlineConsult, @DrExpMonth, @JoinDate, " +
                    "@CreatedBy, @DoctorId, @isExist OUTPUT", // ✅ fixed comma here
                    new SqlParameter("@PhotoUrl", string.IsNullOrEmpty(request.PhotoUrl) ? "-" : request.PhotoUrl),
                    new SqlParameter("@MemberType", request.MemberType),
                    new SqlParameter("@MemberSubType", string.IsNullOrEmpty(request.MemberSubType) ? "-" : request.MemberSubType),
                    new SqlParameter("@IDType", string.IsNullOrEmpty(request.IDType) ? "-" : request.IDType),
                    new SqlParameter("@NRIC", string.IsNullOrEmpty(request.NRIC) ? "-" : request.NRIC),
                    new SqlParameter("@IDProof1", string.IsNullOrEmpty(request.IDProof1) ? "-" : request.IDProof1),
                    new SqlParameter("@IDProof2", string.IsNullOrEmpty(request.IDProof2) ? "-" : request.IDProof2),
                    new SqlParameter("@FullName1", string.IsNullOrEmpty(request.FullName1) ? "-" : request.FullName1),
                    new SqlParameter("@FullName2", string.IsNullOrEmpty(request.FullName2) ? "-" : request.FullName2),
                    new SqlParameter("@RelativeID1", string.IsNullOrEmpty(request.RelativeID1) ? "-" : request.RelativeID1),
                    new SqlParameter("@RelativeID2", string.IsNullOrEmpty(request.RelativeID2) ? "-" : request.RelativeID2),
                    new SqlParameter("@RelativeName1", string.IsNullOrEmpty(request.RelativeName1) ? "-" : request.RelativeName1),
                    new SqlParameter("@RelativeName2", string.IsNullOrEmpty(request.RelativeName2) ? "-" : request.RelativeName2),
                    new SqlParameter("@Relationship1", string.IsNullOrEmpty(request.Relationship1) ? "-" : request.Relationship1),
                    new SqlParameter("@Relationship2", string.IsNullOrEmpty(request.Relationship2) ? "-" : request.Relationship2),
                    new SqlParameter("@DOB", request.DOB ?? DateTime.Now),
                    new SqlParameter("@Gender", request.Gender ?? 'U'),
                    new SqlParameter("@MStatus", request.MStatus ?? 'U'),
                    new SqlParameter("@Nationality", string.IsNullOrEmpty(request.Nationality) ? "-" : request.Nationality),
                    new SqlParameter("@Address1", string.IsNullOrEmpty(request.Address1) ? "-" : request.Address1),
                    new SqlParameter("@Address2", string.IsNullOrEmpty(request.Address2) ? "-" : request.Address2),
                    new SqlParameter("@Address3", string.IsNullOrEmpty(request.Address3) ? "-" : request.Address3),
                    new SqlParameter("@Address4", string.IsNullOrEmpty(request.Address4) ? "-" : request.Address4),
                    new SqlParameter("@EAddress1", string.IsNullOrEmpty(request.EAddress1) ? "-" : request.EAddress1),
                    new SqlParameter("@EAddress2", string.IsNullOrEmpty(request.EAddress2) ? "-" : request.EAddress2),
                    new SqlParameter("@EAddress3", string.IsNullOrEmpty(request.EAddress3) ? "-" : request.EAddress3),
                    new SqlParameter("@PostCode", string.IsNullOrEmpty(request.PostCode) ? "-" : request.PostCode),
                    new SqlParameter("@District", string.IsNullOrEmpty(request.District) ? "-" : request.District),
                    new SqlParameter("@StateName", string.IsNullOrEmpty(request.StateName) ? "-" : request.StateName),
                    new SqlParameter("@Country", string.IsNullOrEmpty(request.Country) ? "-" : request.Country),
                    new SqlParameter("@Email", string.IsNullOrEmpty(request.Email) ? "-" : request.Email),
                    new SqlParameter("@PH1", string.IsNullOrEmpty(request.PH1) ? "-" : request.PH1),
                    new SqlParameter("@PH2", string.IsNullOrEmpty(request.PH2) ? "-" : request.PH2),
                    new SqlParameter("@Blood", string.IsNullOrEmpty(request.Blood) ? "-" : request.Blood),
                    new SqlParameter("@IsPWD", request.IsPWD),
                    new SqlParameter("@Remark", string.IsNullOrEmpty(request.Remark) ? "-" : request.Remark),
                    new SqlParameter("@DrSpecialization", string.IsNullOrEmpty(request.DrSpecialization) ? "-" : request.DrSpecialization),
                    new SqlParameter("@DrDegree", string.IsNullOrEmpty(request.DrDegree) ? "-" : request.DrDegree),
                    new SqlParameter("@DrCertificate", string.IsNullOrEmpty(request.DrCertificate) ? "-" : request.DrCertificate),
                    new SqlParameter("@DrisOnlineConsult", request.DrisOnlineConsult),
                    new SqlParameter("@DrExpMonth", request.DrExpMonth ?? 0),
                    new SqlParameter("@JoinDate", request.JoinDate ?? DateTime.Now),
                    new SqlParameter("@CreatedBy", string.IsNullOrEmpty(request.CreatedBy) ? "-" : request.CreatedBy),
                    new SqlParameter("@DoctorId", string.IsNullOrEmpty(doctorId) ? "-" : doctorId),
                    isExistParam
                );

                var isExist = (bool)isExistParam.Value;

                if (isExist)
                {
                    return Conflict(new { message = "Patient already exists" });
                }

                return Ok(new { message = "Patient created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpGet("check-nric")]
        public async Task<IActionResult> CheckNRICExists([FromQuery] string nric)
        {
            var nricParam = new SqlParameter("@NRIC", nric ?? (object)DBNull.Value);

            var result = await _context.Set<CheckPatientRequest>()
                .FromSqlRaw("EXEC spCheckPatient @NRIC", nricParam)
                .ToListAsync();

            bool exists = result.Any();
            string? memberId = result.FirstOrDefault()?.MemberID;

            return Ok(new { exists, memberId });
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
