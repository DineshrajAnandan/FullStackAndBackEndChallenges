using System;
using System.ComponentModel.DataAnnotations;

namespace POC_GMS_API.Models.DB
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; } // email id
        public string Password { get; set; }
        public string UserType { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

