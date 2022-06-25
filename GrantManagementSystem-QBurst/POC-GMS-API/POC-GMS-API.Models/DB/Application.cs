using System.ComponentModel.DataAnnotations;

namespace POC_GMS_API.Models.DB
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int GrantId { get; set; }
        public int StatusId { get; set; }

        public ApplicantDetail Applicant { get; set; }
        public GrantProgram Grant { get; set; }
    }
}
