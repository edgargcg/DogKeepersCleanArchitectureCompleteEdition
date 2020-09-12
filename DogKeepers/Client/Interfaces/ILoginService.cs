using DogKeepers.Shared.DTOs;
using System.Threading.Tasks;

namespace DogKeepers.Client.Interfaces
{
    public interface ILoginService
    {

        Task Login(JwtDto token);
        Task Logout();
        Task TaskVerifyRefreshToken();

    }
}
