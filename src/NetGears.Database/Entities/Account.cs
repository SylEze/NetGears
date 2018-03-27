namespace NetGears.Database.Entities
{
    public class Account : IEntity
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
