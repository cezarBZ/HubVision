using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HubVision.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "HV";
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

}
