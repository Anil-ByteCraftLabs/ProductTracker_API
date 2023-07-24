namespace ProductTracker.Core.Entities
{
    public class User : Entity
    {

        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
