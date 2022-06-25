using POC_GMS_API.Contracts;
using POC_GMS_API.DataAccess.Data;
using POC_GMS_API.Models.DTO;
using System;
using System.Linq;

namespace POC_GMS_API.DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly GrantsAndApplicantContext _grantsAndApplicantContext;
        private readonly IApplicantService _applicantService;

        public UserService(GrantsAndApplicantContext grantsAndApplicantContext,
            IApplicantService applicantService)
        {
            _grantsAndApplicantContext = grantsAndApplicantContext;
            _applicantService = applicantService;
        }
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _grantsAndApplicantContext.UserInfo.SingleOrDefault(x => x.UserName == username);

            if (user == null) return null;
            if (!password.Equals(user.Password)) return null;

            return new User { 
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                UserType = user.UserType,
                CreateDate = user.CreateDate
            };
        }

        public void Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_grantsAndApplicantContext.UserInfo.Any(x => x.UserName == user.UserName))
                throw new Exception("Username \"" + user.UserName + "\" is already taken");

            var userInfo = new Models.DB.UserInfo
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = password,
                UserType = user.UserType,
                CreateDate = DateTime.UtcNow
            };
            _grantsAndApplicantContext.UserInfo.Add(userInfo);
            _grantsAndApplicantContext.SaveChanges();

            user.Id = userInfo.UserId;
            _applicantService.InsertApplicantAfterRegistrationAsync(user);
        }
    }
}
