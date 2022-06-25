using POC_GMS_API.Models.DTO;

namespace POC_GMS_API.Contracts
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        void Create(User user, string password);
    }
}
