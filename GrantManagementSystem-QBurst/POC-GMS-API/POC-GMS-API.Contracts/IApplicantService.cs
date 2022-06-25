using POC_GMS_API.Models.DB;
using POC_GMS_API.Models.DTO;
using System.Threading.Tasks;

namespace POC_GMS_API.Contracts
{
    public interface IApplicantService
    {
        Task InsertApplicantAfterRegistrationAsync(User detail);
        Task<ApplicantInfo> GetApplicantAsync(int userId);
        Task UpdateApplicantAsync(ApplicantInfo applicantInfo);
        Task ApplyGrantAsync(int userId, int grantId);
        Task UpdateApplicationStatusAsync(int applicantionId, int status);
    }
}
