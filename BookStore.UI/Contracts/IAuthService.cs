using BookStore.Shared;
using System.Threading.Tasks;

namespace BookStore.UI.Contracts
{
    public interface IAuthService
    {
        Task<bool> Register(UserRegisterDto userRegisterDto);
    }
}
