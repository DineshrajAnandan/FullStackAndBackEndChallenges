namespace POC_GMS_API.Models.DTO
{
    public class ReviewScreenDetail
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public int Country { get; set; }
        public int ReviewerStatusId { get; set; }
        public int GrantId { get; set; }
        public string ProgramCode { get; set; }
    }
}