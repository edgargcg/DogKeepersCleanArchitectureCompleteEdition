using System;

namespace DogKeepers.Core.Entities
{
    public class DogPicture
    {

        public int Id { get; set; }
        public bool IsProfile { get; set; }
        public string Picture { get; set; }
        public Byte[] PictureFile { get; set; }

    }
}
