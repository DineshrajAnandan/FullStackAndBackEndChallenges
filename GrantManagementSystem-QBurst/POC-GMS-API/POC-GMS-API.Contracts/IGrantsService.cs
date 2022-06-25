using POC_GMS_API.Models.DB;
using POC_GMS_API.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC_GMS_API.Contracts
{
    public interface IGrantsService
    {
        Task<List<GrantProgram>> GetAllGrantsAsync();
        Task<List<UserGrantsStatus>> GetAllGrantsAndStatusAsync(int userId);
        Task<GrantProgram> AddGrantAsync(GrantProgram grant);
        Task EditGrantAsync(GrantProgram grant);
        Task DeleteGrantAsync(int grantId);
        Task<List<ReviewScreenDetail>> GetReviewScreenDetailForAdminAsync();
    }
}
