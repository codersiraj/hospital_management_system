using System.ComponentModel.DataAnnotations;

namespace HospitalApplicationAPI.Models.Request
{
    public class PatientRequest
    {
        public string PatientName { get; set; }

        public string? BloodGroup { get; set; }

        public DateTime? DOB { get; set; }

        public string? ICType { get; set; }  // Consider enum or lookup later

        public string NRIC { get; set; }

        public string? Nationality { get; set; }

        public string? Religion { get; set; }

        public string? Race { get; set; }  // Consider enum or lookup later

        public string? Language { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? Address3 { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? Pincode { get; set; }

        public string? Phone1 { get; set; }

        public string? Phone2 { get; set; }

        public string? RelativeID1 { get; set; }

        public string? RelativeName1 { get; set; }

        public string? Relationship1 { get; set; }

        public string? RelativeID2 { get; set; }

        public string? RelativeName2 { get; set; }

        public string? Relationship2 { get; set; }

        public string? Email { get; set; }
    }
}
