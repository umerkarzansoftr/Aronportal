using aronportal.Models;
using System.Threading.Tasks;

namespace aronportal.Repositories
{
    public interface IAuthRepository
    {
        Task<TblUser> Register(TblUser user, string password);
        Task<TblUser> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}