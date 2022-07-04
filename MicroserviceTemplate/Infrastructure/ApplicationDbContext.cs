using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MicroserviceTemplate.Domain;


namespace MicroserviceTemplate.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;


        public void Initialize()
        {
            //Permissions.AddRange(GetSeedingMessages());
            SaveChanges();
        }        
        
    }
}
