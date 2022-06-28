using MicroserviceTemplate.Application;
using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql("Server=localhost;Port=5433;Database=Atlasdb-test;User Id=myusername;Password=mypassword"));

                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

                //services.AddScoped<IIncidentService, IncidentService>();

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();


                    if (!db.Incidents.Any())
                    {
                        var pendingMigrations = db.Database.GetPendingMigrations();

                        if (pendingMigrations.Any())
                        {
                            db.Database.Migrate();
                        }
                        try
                        {
                            db.Initialize();
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the " +
                                "database with test messages. Error: {Message}", ex.Message);
                        }
                    }
                        
                }
            });
        }
    }
}