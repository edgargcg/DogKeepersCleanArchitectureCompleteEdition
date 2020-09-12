using DogKeepers.Shared.CustomEntities;

namespace DogKeepers.Shared.QueryFilters
{
    public class DogListQueryFilter : Pagination
    {

        public string Name { get; set; }
        public int RaceId { get; set; }
        public int SizeId { get; set; }
        public int Random { get; set; }

    }
}
