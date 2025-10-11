using System.ComponentModel.DataAnnotations;

namespace HospitalApplicationAPI.Models.Request
{
    public class GetDoctorsRequest
    {
        public string MemberId { get; set; }
        public string FullName1 { get; set; } = string.Empty;

        public string NRIC { get; set; } = string.Empty;

        public string? DrSpecialization { get; set; }

    }
}
