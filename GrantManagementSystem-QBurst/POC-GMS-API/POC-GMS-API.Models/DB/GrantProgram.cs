using System;
using System.ComponentModel.DataAnnotations;

namespace POC_GMS_API.Models.DB
{
    public class GrantProgram
    {
        [Key]
        public int GrantId { get; set; }
        public string GrantProgramName { get; set; }
        public string ProgramCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}
