namespace HospitalApplicationAPI.Models.Response
{
    public class PatientResponse
    {
        public string PatientName { get; set; }

        public string? BloodGroup { get; set; }

        public DateTime? DOB { get; set; }

        public string? ICType { get; set; }  // Consider enum or lookup later

        public string NRIC { get; set; }

        public string? Nationality { get; set; }

        public string? Phone1 { get; set; }

        public string? Email { get; set; }
    }
}
