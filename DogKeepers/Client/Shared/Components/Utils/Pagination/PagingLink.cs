namespace DogKeepers.Client.Shared.Components.Utils.Pagination
{
    public class PagingLink
    {

        public int PageNumber { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnabled { get; set; }

        public PagingLink(int pageNumber, string text, bool isEnabled)
        {
            PageNumber = pageNumber;
            Text = text;
            IsEnabled = isEnabled;
        }

    }
}
