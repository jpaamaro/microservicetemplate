namespace MicroserviceTemplate.Domain
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<Permission> Permissions { get; set; } = null!;
        public DateTime LastUpdated { get; set; }
    }
}