using HubVision.Domain.AggregatesModel.CampaingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.ToTable(nameof(Campaign), Data.ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired();

        builder.Property(c => c.Objective);

        builder.Property(c => c.Status);

        builder.Property(a => a.DailyBudget)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2)
            .HasComment("Orçamento diário");

        builder.Property(a => a.LifetimeBudget)
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2)
            .HasComment("Orçamento vitalício (opcional)");

        builder.Property(c => c.StartTime);

        builder.Property(c => c.EndTime);

        builder.Property(c => c.AdAccountId).IsRequired();
        builder.HasOne(c => c.AdAccount)
            .WithMany(c => c.Campaigns)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.CreatedAt).IsRequired();

        builder.Property(c => c.UpdatedAt);

        builder.HasMany(c => c.AdSets)
            .WithOne(c => c.Campaign)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
