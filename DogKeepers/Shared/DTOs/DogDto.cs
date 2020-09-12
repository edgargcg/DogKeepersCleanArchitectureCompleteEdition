using DogKeepers.Shared.CustomEntities;
using System;

namespace DogKeepers.Shared.DTOs
{
    public class DogDto : FileExtension
    {

        public Int64 Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Conditions { get; set; }
        public string Description { get; set; }
        public int RaceId { get; set; }
        public byte[] PictureFile { get; set; }


        public RaceDto Race { get; set; }
        public SizeDto Size { get; set; }

    }
}
