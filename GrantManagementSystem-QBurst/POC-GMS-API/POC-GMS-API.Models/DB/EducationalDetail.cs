using System.ComponentModel.DataAnnotations;

namespace POC_GMS_API.Models.DB
{
    public class EducationalDetail
    {
        [Key]
        public int EducationalDetailId { get; set; }
        public int ApplicantId { get; set; }
        public string CourseName { get; set; }
        public string Country { get; set; }
        public string InstitutionName { get; set; }
        public int YearOfCompletion { get; set; }

        public ApplicantDetail Applicant { get; set; }
    }
}







