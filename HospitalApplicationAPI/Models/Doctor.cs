using HospitalApplicationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Doctor
{

    [Key, ForeignKey("User")] // ✅ Make UserId the primary key and foreign key
    public string UserId { get; set; } = null!;

    public string? Specialization { get; set; }
    public string? Availability { get; set; }
    public string? Qualification { get; set; }
    public int ExperienceYears { get; set; }
    public string? Bio { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
}
