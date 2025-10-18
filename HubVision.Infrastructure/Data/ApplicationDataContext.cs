using HubVision.Domain.AggregatesModel.AdAccountAggregate;
using HubVision.Domain.AggregatesModel.AdAggregate;
using HubVision.Domain.AggregatesModel.AdSetAggregate;
using HubVision.Domain.AggregatesModel.AgencyAggregate;
using HubVision.Domain.AggregatesModel.CampaingAggregate;
using HubVision.Domain.AggregatesModel.ClientAggregate;
using HubVision.Domain.AggregatesModel.PlatformAccountAggregate;
using HubVision.Domain.AggregatesModel.TrafficManagerAggregate;
using HubVision.Domain.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace HubVision.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "HV";

    private IDbContextTransaction _transaction;

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

    public async Task<int> CommitAsync()
    {
        return await SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public override void Dispose()
    {
        _transaction?.Dispose();
        base.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
        }
        await base.DisposeAsync();
    }
}