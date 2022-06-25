using Microsoft.EntityFrameworkCore;
using POC_GMS_API.Contracts;
using POC_GMS_API.DataAccess.Data;
using POC_GMS_API.Models;
using POC_GMS_API.Models.DB;
using POC_GMS_API.Models.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace POC_GMS_API.DataAccess.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly GrantsAndApplicantContext _grantsAndApplicantContext;

        public ApplicantService(GrantsAndApplicantContext grantsAndApplicantContext)
        {
            _grantsAndApplicantContext = grantsAndApplicantContext;
        }

        //this should be called when user registers
        public async Task InsertApplicantAfterRegistrationAsync(User detail)
        {
            var applicantToInsert = new ApplicantDetail
            {
                Email = detail.UserName,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                UserId = detail.Id
            };
            _grantsAndApplicantContext.ApplicantDetail.Add(applicantToInsert);
            await _grantsAndApplicantContext.SaveChangesAsync();
        }

        public async Task<ApplicantInfo> GetApplicantAsync(int userId)
        {
            var applicant = _grantsAndApplicantContext.ApplicantDetail
                .AsNoTracking()
                .First(applicant => applicant.UserId.Equals(userId));
            var educationDetails = _grantsAndApplicantContext.EducationalDetail
                .AsNoTracking()
                .Where(x => x.ApplicantId == applicant.ApplicantId)
                .ToList();

            //_grantsAndApplicantContext.ApplicantDetail.Join(
            //        _grantsAndApplicantContext.EducationalDetail,
            //        o => o.ApplicantId,
            //        i => i.ApplicantId,
            //        (o,i) => new
            //        {

            //        }
            //    ).Where(o => o.)
            var applicantInfo = new ApplicantInfo
            {
                ApplicantId = applicant.ApplicantId,
                UserId = applicant.UserId,
                FirstName = applicant.FirstName,
                LastName = applicant.LastName,
                Email = applicant.Email,
                DateofBirth = applicant.DateofBirth,
                Country = applicant.Country,
                State = applicant.State,
                PhysicallyDisabled = applicant.PhysicallyDisabled,
                Address = applicant.Address,
                City = applicant.City,
                PostalCode = applicant.PostalCode,
                Mobile = applicant.Mobile,
                Phone = applicant.Phone,
                EducationalDetails = educationDetails
            };
            return applicantInfo;
        }

        public async Task UpdateApplicantAsync(ApplicantInfo applicantInfo)
        {
            var applicant = _grantsAndApplicantContext.ApplicantDetail.Find(applicantInfo.ApplicantId);
            if (applicant != null)
            {
                applicant.FirstName = applicantInfo.FirstName;
                applicant.LastName = applicantInfo.LastName;
                applicant.PhysicallyDisabled = applicantInfo.PhysicallyDisabled;
                applicant.DateofBirth = applicantInfo.DateofBirth;
                applicant.Country = applicantInfo.Country;
                applicant.State = applicantInfo.State;
                applicant.Address = applicantInfo.Address;
                applicant.PostalCode = applicantInfo.PostalCode;
                applicant.City = applicantInfo.City;
                applicant.Mobile = applicantInfo.Mobile;
                applicant.Phone = applicantInfo.Phone;
            }

            foreach (var ed in applicantInfo.EducationalDetails)
            {
                var eduDetail = _grantsAndApplicantContext.EducationalDetail.Find(ed.EducationalDetailId);
                if(eduDetail == null)
                {
                    _grantsAndApplicantContext.EducationalDetail.Add(new EducationalDetail
                    {
                        ApplicantId = ed.ApplicantId,
                        CourseName = ed.CourseName,
                        Country = ed.Country,
                        InstitutionName = ed.InstitutionName,
                        YearOfCompletion = ed.YearOfCompletion
                    });
                }
                else
                {
                    eduDetail.CourseName = ed.CourseName;
                    eduDetail.ApplicantId = ed.ApplicantId;
                    eduDetail.Country = ed.Country;
                    eduDetail.YearOfCompletion = ed.YearOfCompletion;
                    eduDetail.InstitutionName = ed.InstitutionName;
                }
            }

            await _grantsAndApplicantContext.SaveChangesAsync();

        }

        public async Task ApplyGrantAsync(int userId, int grantId)
        {
            var applicant = _grantsAndApplicantContext.ApplicantDetail.First(x => x.UserId == userId);
            if(applicant == null)
            {
                throw new Exception("Invalid user Id"); 
            }
            var grant = _grantsAndApplicantContext.GrantProgram.Find(grantId);
            if (applicant != null && grant != null && grant.Status)
            {
                var application = _grantsAndApplicantContext.Application.FirstOrDefault(x => x.ApplicantId == applicant.ApplicantId && x.GrantId == grantId);
                if (application == null)
                {
                    var newApplication = new Application
                    {
                        GrantId = grantId,
                        ApplicantId = (int)applicant.ApplicantId,
                        StatusId = 1 //submitted
                    };
                    _grantsAndApplicantContext.Application.Add(newApplication);
                    await _grantsAndApplicantContext.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateApplicationStatusAsync(int applicantionId, int status)
        {
            var application = _grantsAndApplicantContext.Application.Find(applicantionId);
            if(application != null)
            {
                application.StatusId = status;
                await _grantsAndApplicantContext.SaveChangesAsync();
            }
        }
    }
}
