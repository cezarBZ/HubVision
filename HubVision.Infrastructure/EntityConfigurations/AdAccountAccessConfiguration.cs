using HubVision.Domain.AggregatesModel.AdAccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class AdAccountAccessConfiguration : IEntityTypeConfiguration<AdAccountAccess>
{
    public void Configure(EntityTypeBuilder<AdAccountAccess> builder)
    {
        builder.ToTable(nameof(AdAccountAccess), Data.ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AdAccountId)
            .IsRequired();

        builder.Property(a => a.TrafficManagerId)
            .IsRequired();

        builder.Property(a => a.AccessLevel)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.GrantedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(a => a.GrantedBy)
            .IsRequired();

        builder.HasIndex(a => new { a.AgencyId, a.AdAccountId })
            .HasDatabaseName("IX_AdAccountAccess_AgencyId_AdAccountId");

        builder.HasIndex(a => new { a.AgencyId, a.TrafficManagerId })
            .HasDatabaseName("IX_AdAccountAccess_AgencyId_TrafficManagerId");

        builder.HasIndex(a => a.GrantedAt)
            .HasDatabaseName("IX_AdAccountAccess_GrantedAt");

        builder.HasOne(a => a.AdAccount)
            .WithMany(aa => aa.AdAccountAccesses)
            .HasForeignKey(a => a.AdAccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_AdAccountAccess_AdAccounts");

        builder.HasOne(a => a.TrafficManager)
            .WithMany(tm => tm.AdAccountAccesses)
            .HasForeignKey(a => a.TrafficManagerId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_AdAccountAccess_TrafficManagers");
    }
}
