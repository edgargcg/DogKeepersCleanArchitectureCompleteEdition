using DogKeepers.Core.Entities;

namespace DogKeepers.Core.Interfaces.Utils
{
    public interface IJwtUtil
    {

        Jwt Generate(User user);

    }
}
