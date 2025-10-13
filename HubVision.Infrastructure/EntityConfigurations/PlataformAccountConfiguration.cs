using HubVision.Domain.AggregatesModel.PlatformAccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class PlatformAccountConfiguration : IEntityTypeConfiguration<PlatformAccount>
{
    public void Configure(EntityTypeBuilder<PlatformAccount> builder)
    {
        builder.ToTable("PlatformAccount", Data.ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PlatformUserId)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.PlatformEmail)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.DisplayName)
            .HasMaxLength(255);

        builder.Property(p => p.AccessToken)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(p => p.RefreshToken)
            .HasMaxLength(2000);

        builder.Property(p => p.TokenExpiresAt)
            .HasColumnType("timestamp with time zone");

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.IsDefault)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.ConnectedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");

        builder.Property(p => p.LastSyncedAt)
            .HasColumnType("timestamp with time zone");

        builder.Property(p => p.Platform)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(p => p.PlatformMetadata)
            .HasColumnType("jsonb");

        builder.HasIndex(p => new { p.AgencyId, p.TrafficManagerId })
            .HasDatabaseName("IX_PlatformAccounts_AgencyId_TrafficManagerId");

        builder.HasIndex(p => new { p.Platform, p.PlatformUserId })
            .HasDatabaseName("IX_PlatformAccounts_Platform_PlatformUserId");

        builder.HasIndex(p => new { p.AgencyId, p.Platform, p.IsActive })
            .HasDatabaseName("IX_PlatformAccounts_AgencyId_Platform_IsActive");

        builder.HasIndex(p => p.TrafficManagerId)
            .HasDatabaseName("IX_PlatformAccounts_TrafficManagerId");

        builder.HasIndex(p => p.PlatformEmail)
            .HasDatabaseName("IX_PlatformAccounts_PlatformEmail");

        builder.HasIndex(p => p.PlatformMetadata)
            .HasDatabaseName("IX_PlatformAccounts_PlatformMetadata")
            .HasMethod("gin");

        builder.HasOne(p => p.Agency)
            .WithMany()
            .HasForeignKey(p => p.AgencyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_PlatformAccounts_Agencies");

        builder.HasOne(p => p.TrafficManager)
            .WithMany(t => t.PlatformAccounts)
            .HasForeignKey(p => p.TrafficManagerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired()
            .HasConstraintName("FK_PlatformAccounts_TrafficManagers");

        builder.HasMany(p => p.AdAccounts)
            .WithOne(a => a.PlatformAccount)
            .HasForeignKey(a => a.PlatformAccountId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_AdAccounts_PlatformAccounts");

        builder.Navigation(p => p.AdAccounts)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .AutoInclude(false);
    }
}
