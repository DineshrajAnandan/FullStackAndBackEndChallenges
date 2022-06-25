using POC_GMS_API.Models.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC_GMS_API.Models.DTO
{
    public class ApplicantInfo
    {
        public int ApplicantId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateofBirth { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public bool PhysicallyDisabled { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public List<EducationalDetail> EducationalDetails { get; set; }
    }
}
