using DogKeepers.Core.Interfaces.Utils;

namespace DogKeepers.Infrastructure.Utils
{
    public class FileUtil : IFileUtil
    {
        public byte[] GetFile(string path)
        {
            var imageBytes = System.IO.File.ReadAllBytes(path);

            return imageBytes;
        }
    }
}
