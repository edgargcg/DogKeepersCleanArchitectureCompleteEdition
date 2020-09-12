namespace DogKeepers.Shared.Metadata
{
    public class PaginationMetadata
    {

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public int? NextPageNumber { get; set; }
        public int? PreviousPageNumber { get; set; }


    }
}
