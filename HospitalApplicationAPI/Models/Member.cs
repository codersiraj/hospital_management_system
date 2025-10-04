public class Member
{
    public string MemberId { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public string MemberType { get; set; } = string.Empty;
    public string? MemberSubType { get; set; }
    public string IDType { get; set; } = string.Empty;
    public string NRIC { get; set; } = string.Empty;
    public string? IDProof1 { get; set; }
    public string? IDProof2 { get; set; }
    public string FullName1 { get; set; } = string.Empty;
    public string? FullName2 { get; set; }
    public string? RelativeID1 { get; set; }
    public string? RelativeID2 { get; set; }
    public string? RelativeName1 { get; set; }
    public string? RelativeName2 { get; set; }
    public string? Relationship1 { get; set; }
    public string? Relationship2 { get; set; }

    public DateTime? DOB { get; set; }   // ✅ nullable
    public string? Gender { get; set; }
    public string? MStatus { get; set; }
    public string? Nationality { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? Address4 { get; set; }

    public string? EAddress1 { get; set; }
    public string? EAddress2 { get; set; }
    public string? EAddress3 { get; set; }

    public string? PostCode { get; set; }
    public string? District { get; set; }
    public string? StateName { get; set; }
    public string? Country { get; set; }

    public string? Email { get; set; }   // ✅ nullable
    public string? PH1 { get; set; }
    public string? PH2 { get; set; }

    public string? Blood { get; set; }
    public bool IsPWD { get; set; }
    public string? Remark { get; set; }

    public string? DrSpecialization { get; set; }
    public string? DrDegree { get; set; }
    public string? DrCertificate { get; set; }
    public bool DrisOnlineConsult { get; set; }
    public int? DrExpMonth { get; set; }

    public DateTime? JoinDate { get; set; }
    public bool IsNew { get; set; }
    public bool Sync { get; set; }
    public string Life { get; set; } = "1";

    public DateTime CreatedAt { get; set; }
    public DateTime EditedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
