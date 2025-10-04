using HospitalApplicationAPI.Data;
using HospitalApplicationAPI.Models;
using HospitalApplicationAPI.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HospitalApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserAccountController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a new User Account (spInsertUserAccount)
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAccount([FromBody] UserAccount request)
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
                    "EXEC dbo.spInsertUserAccount @UserID, @MemberID, @UserType, @Pwd, @CreatedBy, @isExist OUTPUT",
                    new SqlParameter("@UserID", request.UserID),
                    new SqlParameter("@MemberID", request.MemberID),
                    new SqlParameter("@UserType", request.UserType),
                    new SqlParameter("@Pwd", request.Pwd), // ⚡ hash before storing in real app
                    new SqlParameter("@CreatedBy", request.CreatedBy ?? "system"),
                    isExistParam
                );

                bool isExist = (bool)isExistParam.Value;

                if (isExist)
                    return Conflict(new { message = "User already exists." });

                return Ok(new { message = "User account created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete (soft delete) a user account using stored procedure spDeleteUserAccount
        /// </summary>
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUserAccount(string userId, [FromQuery] string editedBy = "system")
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.spDeleteUserAccount @UserID, @EditedBy",
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@EditedBy", editedBy)
                );

                return Ok(new { message = $"User account {userId} deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get user details by UserID (calls dbo.spFindUser)
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                // Call stored procedure and fetch user list
                var users = await _context.UserAccounts
                    .FromSqlRaw("EXEC dbo.spFindUser @UserID", new SqlParameter("@UserID", userId))
                    .AsNoTracking()
                    .ToListAsync();

                var user = users.FirstOrDefault();

                if (user == null)
                    return NotFound(new { message = "User not found." });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get UserID by MemberID (calls dbo.spFindUserByMemberID)
        /// </summary>
        [HttpGet("by-member/{memberId}")]
        public async Task<IActionResult> GetUserByMemberId(string memberId)
        {
            try
            {
                var result = await _context.UserAccounts
                    .FromSqlRaw("EXEC dbo.spFindUserByMemberID @MemberID", new SqlParameter("@MemberID", memberId))
                    .AsNoTracking()
                    .ToListAsync();   // async EF query execution

                var user = result.Select(u => new { u.UserID }).FirstOrDefault();

                if (user == null)
                    return NotFound(new { message = "User not found for the given MemberID." });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
