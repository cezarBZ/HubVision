using HubVision.Domain.AggregatesModel.AdAccountAggregate;
using HubVision.Domain.AggregatesModel.AdAggregate;
using HubVision.Domain.AggregatesModel.AdSetAggregate;
using HubVision.Domain.AggregatesModel.AgencyAggregate;
using HubVision.Domain.AggregatesModel.CampaingAggregate;
using HubVision.Domain.AggregatesModel.ClientAggregate;
using HubVision.Domain.AggregatesModel.PlatformAccountAggregate;
using HubVision.Domain.AggregatesModel.TrafficManagerAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HubVision.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "HV";
    public DbSet<Agency> Agencies { get; set; }
    public DbSet<AdAccount> AdAccounts { get; set; }
    public DbSet<AdSet> adSets { get; set; }
    public DbSet<Ad> Ads { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<PlatformAccount> PlatformAccounts { get; set; }
    public DbSet<TrafficManager> TrafficManagers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

}
