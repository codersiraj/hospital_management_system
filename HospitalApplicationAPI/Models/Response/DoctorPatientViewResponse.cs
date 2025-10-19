using System;

namespace HospitalApplicationAPI.Models.Response
{
    public class DoctorPatientView
    {
        public string TokenNo { get; set; }
        public string PatientID { get; set; } = string.Empty;
        public DateTime? CallTime { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
    }
}
