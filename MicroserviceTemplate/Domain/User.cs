namespace MicroserviceTemplate.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<Profile> Profiles { get; set; } = null!;
        public DateTime LastUpdated { get; set; }

    }
}
