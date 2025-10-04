using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApplicationAPI.Models
{
    public class UserAccount
    {
        [Key]
        public string UserID { get; set; } = string.Empty;
        public string MemberID { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty;  // recommend hashing before storing
        public bool IsActive { get; set; }
        public bool IsNew { get; set; }
        public bool Sync { get; set; }
        public char Life { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string EditedBy { get; set; } = string.Empty;
    }
}
