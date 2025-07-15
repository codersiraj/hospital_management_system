using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApplicationAPI.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PatientName { get; set; }

        [MaxLength(5)]
        public string? BloodGroup { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [MaxLength(20)]
        public string? ICType { get; set; }  // Consider enum or lookup later

        [Required]
        [MaxLength(50)]
        public string NRIC { get; set; }

        [MaxLength(50)]
        public string? Nationality { get; set; }

        [MaxLength(50)]
        public string? Religion { get; set; }

        [MaxLength(20)]
        public string? Race { get; set; }  // Consider enum or lookup later

        [MaxLength(50)]
        public string? Language { get; set; }

        [MaxLength(255)]
        public string? Address1 { get; set; }

        [MaxLength(255)]
        public string? Address2 { get; set; }

        [MaxLength(255)]
        public string? Address3 { get; set; }

        [MaxLength(50)]
        public string? State { get; set; }

        [MaxLength(50)]
        public string? Country { get; set; }

        [MaxLength(10)]
        public string? Pincode { get; set; }

        [MaxLength(15)]
        public string? Phone1 { get; set; }

        [MaxLength(15)]
        public string? Phone2 { get; set; }

        public string? RelativeID1 { get; set; }

        [MaxLength(100)]
        public string? RelativeName1 { get; set; }

        [MaxLength(50)]
        public string? Relationship1 { get; set; }

        public string? RelativeID2 { get; set; }

        [MaxLength(100)]
        public string? RelativeName2 { get; set; }

        [MaxLength(50)]
        public string? Relationship2 { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
