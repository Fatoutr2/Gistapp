using Gistapp.Models;
using System.Threading.Tasks;

namespace Gistapp.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(ApplicationUser user);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByUserNameAsync(string userName);
    }
}
