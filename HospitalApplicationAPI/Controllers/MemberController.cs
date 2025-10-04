using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApplicationAPI.Data;
using HospitalApplicationAPI.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace HospitalApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MemberController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMember([FromBody] MemberRequest request)
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
                    "EXEC dbo.spInsertMember " +
                    "@PhotoUrl, @MemberType, @MemberSubType, @IDType, @NRIC, @IDProof1, @IDProof2, " +
                    "@FullName1, @FullName2, @RelativeID1, @RelativeID2, @RelativeName1, @RelativeName2, @Relationship1, @Relationship2, " +
                    "@DOB, @Gender, @MStatus, @Nationality, @Address1, @Address2, @Address3, @Address4, @EAddress1, @EAddress2, @EAddress3, " +
                    "@PostCode, @District, @StateName, @Country, @Email, @PH1, @PH2, @Blood, @IsPWD, @Remark, " +
                    "@DrSpecialization, @DrDegree, @DrCertificate, @DrisOnlineConsult, @DrExpMonth, @JoinDate, @IsNew, @Sync, @Life, " +
                    "@CreatedAt, @EditedAt, @CreatedBy, @isExist OUTPUT",
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
                    new SqlParameter("@DOB", request.DOB ?? DateTime.Now), // fallback current date if null
                    new SqlParameter("@Gender", request.Gender ?? 'U'), // U = Unknown
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
                    new SqlParameter("@IsNew", request.IsNew),
                    new SqlParameter("@Sync", request.Sync),
                    new SqlParameter("@Life", request.Life),
                    new SqlParameter("@CreatedAt", request.CreatedAt),
                    new SqlParameter("@EditedAt", request.EditedAt),
                    new SqlParameter("@CreatedBy", string.IsNullOrEmpty(request.CreatedBy) ? "-" : request.CreatedBy),
                    isExistParam
                );

                var isExist = (bool)isExistParam.Value;

                if (isExist)
                {
                    return Conflict(new { message = "Member already exists" });
                }

                return Ok(new { message = "Member created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetMembers()
        {
            try
            {
                var members = await _context.Members
                    .FromSqlRaw("EXEC dbo.spGetMembers")
                    .ToListAsync();

                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateMember(string id, [FromBody] MemberRequest request)
        {
            try
            {
                var isExistParam = new SqlParameter
                {
                    ParameterName = "@isExist",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.spUpdateMember " +
                    "@MemberID, @PhotoUrl, @MemberSubType, @IDType, @NRIC, @IDProof1, @IDProof2, " +
                    "@PreviewName1, @PreviewName2, @FullName1, @FullName2, @RelativeID1, @RelativeID2, @RelativeName1, @RelativeName2, " +
                    "@Relationship1, @Relationship2, @DOB, @Gender, @MStatus, @Nationality, @Address1, @Address2, @Address3, @Address4, " +
                    "@EAddress1, @EAddress2, @EAddress3, @PostCode, @District, @StateName, @Country, @Email, @PH1, @PH2, @Blood, " +
                    "@IsPWD, @Remark, @DrSpecialization, @DrDegree, @DrCertificate, @DrisOnlineConsult, @DrExpMonth, @JoinDate, @Life, " +
                    "@EditedBy, @isExist OUTPUT",
                    new SqlParameter("@MemberID", id),
                    new SqlParameter("@PhotoUrl", string.IsNullOrEmpty(request.PhotoUrl) ? "-" : request.PhotoUrl),
                    new SqlParameter("@MemberSubType", string.IsNullOrEmpty(request.MemberSubType) ? "-" : request.MemberSubType),
                    new SqlParameter("@IDType", string.IsNullOrEmpty(request.IDType) ? "-" : request.IDType),
                    new SqlParameter("@NRIC", string.IsNullOrEmpty(request.NRIC) ? "-" : request.NRIC),
                    new SqlParameter("@IDProof1", string.IsNullOrEmpty(request.IDProof1) ? "-" : request.IDProof1),
                    new SqlParameter("@IDProof2", string.IsNullOrEmpty(request.IDProof2) ? "-" : request.IDProof2),
                    new SqlParameter("@PreviewName1", string.IsNullOrEmpty(request.FullName1) ? "-" : request.FullName1), // optional preview
                    new SqlParameter("@PreviewName2", string.IsNullOrEmpty(request.FullName2) ? "-" : request.FullName2),
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
                    new SqlParameter("@IsPWD", request.IsPWD ? 1 : 0),
                    new SqlParameter("@Remark", string.IsNullOrEmpty(request.Remark) ? "-" : request.Remark),
                    new SqlParameter("@DrSpecialization", string.IsNullOrEmpty(request.DrSpecialization) ? "-" : request.DrSpecialization),
                    new SqlParameter("@DrDegree", string.IsNullOrEmpty(request.DrDegree) ? "-" : request.DrDegree),
                    new SqlParameter("@DrCertificate", string.IsNullOrEmpty(request.DrCertificate) ? "-" : request.DrCertificate),
                    new SqlParameter("@DrisOnlineConsult", request.DrisOnlineConsult ? 1 : 0),
                    new SqlParameter("@DrExpMonth", request.DrExpMonth ?? 0),
                    new SqlParameter("@JoinDate", request.JoinDate ?? DateTime.Now),
                    new SqlParameter("@Life", request.Life),
                    new SqlParameter("@EditedBy", string.IsNullOrEmpty(request.CreatedBy) ? "system" : request.CreatedBy),
                    isExistParam
                );

                bool isExist = (bool)isExistParam.Value;
                if (isExist)
                {
                    return Conflict(new { message = "Duplicate NRIC/Email/Phone exists" });
                }

                return Ok(new { message = "Member updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(string id)
        {
            try
            {
                var members = await _context.Members
                    .FromSqlRaw("EXEC dbo.spFindMember @p0", id)
                    .AsNoTracking()
                    .ToListAsync();

                var member = members.FirstOrDefault();

                if (member == null)
                {
                    return NotFound(new { message = "Member not found" });
                }

                return Ok(member);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMember(string id, [FromQuery] string editedBy = "system")
        {
            try
            {
                var isExistParam = new SqlParameter
                {
                    ParameterName = "@isExist",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.spDeleteMember @MemberID, @EditedBy, @isExist OUTPUT",
                    new SqlParameter("@MemberID", id),
                    new SqlParameter("@EditedBy", string.IsNullOrEmpty(editedBy) ? "system" : editedBy),
                    isExistParam
                );

                bool isExist = (bool)isExistParam.Value;

                if (isExist)
                {
                    return Conflict(new { message = "Member cannot be deleted because it is linked with existing cases." });
                }

                return Ok(new { message = "Member deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}
