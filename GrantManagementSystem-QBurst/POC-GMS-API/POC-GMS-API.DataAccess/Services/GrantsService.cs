using Microsoft.EntityFrameworkCore;
using POC_GMS_API.Contracts;
using POC_GMS_API.DataAccess.Data;
using POC_GMS_API.Models.DB;
using POC_GMS_API.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC_GMS_API.DataAccess.Services
{
    public class GrantsService : IGrantsService
    {
        private readonly GrantsAndApplicantContext _grantsAndApplicantContext;

        public GrantsService(GrantsAndApplicantContext grantsAndApplicantContext)
        {
            _grantsAndApplicantContext = grantsAndApplicantContext;
        }
        public async Task<List<GrantProgram>> GetAllGrantsAsync()
        {
            return _grantsAndApplicantContext.GrantProgram.AsNoTracking().ToList();
        }

        public async Task<GrantProgram> AddGrantAsync(GrantProgram grant)
        {
            var grantProgramToInsert = new GrantProgram
            {
                GrantProgramName = grant.GrantProgramName,
                ProgramCode = grant.ProgramCode,
                StartDate = grant.StartDate,
                EndDate = grant.EndDate,
                Status = grant.Status
            };
            _grantsAndApplicantContext.GrantProgram.Add(grantProgramToInsert);
            await _grantsAndApplicantContext.SaveChangesAsync();
            return grantProgramToInsert;
        }

        public async Task EditGrantAsync(GrantProgram grant)
        {
            var grantToEdit = _grantsAndApplicantContext.GrantProgram
                .Find(grant.GrantId);

            grantToEdit.GrantProgramName = grant.GrantProgramName;
            grantToEdit.ProgramCode = grant.ProgramCode;
            grantToEdit.StartDate = grant.StartDate;
            grantToEdit.EndDate = grant.EndDate;
            grantToEdit.Status = grant.Status;

            await _grantsAndApplicantContext.SaveChangesAsync();
        }

        public async Task DeleteGrantAsync(int grantId)
        {
            var grant = _grantsAndApplicantContext.GrantProgram
                .Find(grantId);
            if (grant != null)
            {
                _grantsAndApplicantContext.GrantProgram.Remove(grant);
                await _grantsAndApplicantContext.SaveChangesAsync();
            }
        }

        public async Task<List<UserGrantsStatus>> GetAllGrantsAndStatusAsync(int userId)
        {
            var applicantId = _grantsAndApplicantContext.ApplicantDetail.AsNoTracking().FirstOrDefault(x => x.UserId == userId)?.ApplicantId;
            var applications = _grantsAndApplicantContext.Application.AsNoTracking()
                .Where(x => x.ApplicantId == applicantId)
                .ToList();
            var grants = _grantsAndApplicantContext.GrantProgram.AsNoTracking().ToList();

            var result = new List<UserGrantsStatus>();
            foreach (var grant in grants)
            {
                var applicationStatusId = applications.FirstOrDefault(x => x.GrantId == grant.GrantId)?.StatusId;
                result.Add(new UserGrantsStatus
                {
                    GrantId = grant.GrantId,
                    GrantProgramName = grant.GrantProgramName,
                    ProgramCode = grant.ProgramCode,
                    StartDate = grant.StartDate,
                    EndDate = grant.EndDate,
                    Status = grant.Status,
                    ApplicantUserId = userId,
                    ApplicationStatus = applicationStatusId switch
                    {
                        1 => "submitted",
                        2 => "approved",
                        3 => "rejected",
                        _ => ""
                    }
                });
            }

            return result;
        }

        public async Task<List<ReviewScreenDetail>> GetReviewScreenDetailForAdminAsync()
        {
            var list = _grantsAndApplicantContext.Application.Join(
                    _grantsAndApplicantContext.ApplicantDetail,
                    o => o.ApplicantId,
                    i => i.ApplicantId,
                    (o, i) => new
                    {
                        Id = o.Id,
                        ApplicantName = $"{i.FirstName} {i.LastName}",
                        Country = i.Country,
                        ReviewerStatusId = o.StatusId,
                        GrantId = o.GrantId
                    }
                ).Join(
                    _grantsAndApplicantContext.GrantProgram,
                    o => o.GrantId,
                    i => i.GrantId,
                    (o, i) => new ReviewScreenDetail
                    {
                        Id = o.Id,
                        ApplicantName = o.ApplicantName,
                        Country = o.Country,
                        ReviewerStatusId = o.ReviewerStatusId,
                        GrantId = o.GrantId,
                        ProgramCode = i.ProgramCode
                    }
                ).ToList();

            return list;
        }
    }
}
