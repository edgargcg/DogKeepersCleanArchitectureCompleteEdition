namespace DogKeepers.Shared.CustomEntities
{
    public class Pagination
    {

        private int? _pageNumber { get; set; }
        private int? _pageSize { get; set; }

        public int? PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = _pageNumber ?? value; }
        }

        public int? ForcePageNumber
        {
            set { _pageNumber = value; }
        }

        public int? PageSize
        {
            get { return _pageSize; }
            set { _pageSize = _pageSize ?? value; }
        }

    }
}
