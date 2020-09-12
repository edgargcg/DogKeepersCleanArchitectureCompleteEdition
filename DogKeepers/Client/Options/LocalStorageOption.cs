namespace DogKeepers.Client.Options
{
    public class LocalStorageOption
    {

        public string Token { get; set; }
        public string ExpirationDate { get; set; }
        public int MinutesLeftToRefresh { get; set; }

    }
}
