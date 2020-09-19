using DogKeepers.Core.Entities;
using System.Collections.Generic;

namespace DogKeepers.Core.Interfaces.Utils
{
    public interface IJwtUtil
    {

        Jwt Generate(dynamic data);

    }
}
