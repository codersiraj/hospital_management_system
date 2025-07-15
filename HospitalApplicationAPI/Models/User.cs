using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalApplicationAPI.Models
{

        public class User
        {
            [Key]
            public string UserId { get; set; }

            [Required]
            public string FullName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            public string? Phone { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string Role { get; set; }

            public string CreatedBy { get; set; }

            [Required]
            public DateTime CreatedAt { get; set; }

            [Required]
            public bool IsActive { get; set; }
        }
    }

