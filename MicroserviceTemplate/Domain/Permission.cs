namespace MicroserviceTemplate.Domain
{
    public class Permission
    {
       public Guid Id { get; set; }
       public string Name { get; set; } = null!;
       public DateTime LastUpdated { get; set; }

    }
}