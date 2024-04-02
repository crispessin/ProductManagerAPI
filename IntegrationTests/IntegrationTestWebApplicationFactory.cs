using API;
using Infra.Data.Persistence.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>
    {          
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {            
            builder.ConfigureServices(services =>
            {
                var existingDbContextRegistration = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (existingDbContextRegistration != null)
                {
                    services.Remove(existingDbContextRegistration);
                }

                var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "produto-test" };
                var connectionString = connectionStringBuilder.ToString();
                services
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

                var sp = services.BuildServiceProvider();

                var context = sp.GetRequiredService<ApplicationDbContext>();

                context.Database.EnsureDeleted();                
            });
        }
    }
}
