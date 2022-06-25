using System;

namespace POC_GMS_API.Models.DTO
{
    public class UserGrantsStatus
    {
        public int GrantId { get; set; }
        public string GrantProgramName { get; set; }
        public string ProgramCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public int ApplicantUserId { get; set; }
        public string ApplicationStatus { get; set; }
    }
}
