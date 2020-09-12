using System;
using System.Reflection;

namespace DogKeepers.Core.Entities
{
    public class Dog
    {

        public Int64 Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Conditions { get; set; }
        public string Description { get; set; }
        public int RaceId { get; set; }

        public Race Race { get; set; }
        public Size Size { get; set; }

        public DogPicture Picture { get; set; }

    }
}
