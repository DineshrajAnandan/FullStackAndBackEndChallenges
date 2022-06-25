using System;

namespace POC_GMS_API.Models.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; } // email
        public string UserType { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
