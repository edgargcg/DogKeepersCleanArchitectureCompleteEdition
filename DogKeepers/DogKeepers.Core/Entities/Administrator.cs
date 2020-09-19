namespace DogKeepers.Core.Entities
{
    public class Administrator
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

    }
}
