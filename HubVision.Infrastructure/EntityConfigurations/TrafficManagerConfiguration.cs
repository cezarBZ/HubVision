using HubVision.Domain.AggregatesModel.TrafficManagerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class TrafficManagerConfiguration : IEntityTypeConfiguration<TrafficManager>
{
    public void Configure(EntityTypeBuilder<TrafficManager> builder)
    {
        builder.ToTable("TrafficManager", Data.ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(t => t.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.Phone)
            .HasMaxLength(20);

        builder.Property(t => t.PhotoUrl)
            .HasMaxLength(500);

        builder.Property(t => t.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasSentinel(0)
            .HasDefaultValue(TrafficManagerRole.Junior);

        builder.Property(t => t.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(t => t.HiredAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");

        builder.Property(t => t.LastLoginAt)
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(t => new { t.AgencyId, t.Email })
            .IsUnique()
            .HasDatabaseName("IX_TrafficManagers_AgencyId_Email");

        builder.HasIndex(t => t.Email)
            .HasDatabaseName("IX_TrafficManagers_Email");

        builder.HasIndex(t => new { t.AgencyId, t.IsActive })
            .HasDatabaseName("IX_TrafficManagers_AgencyId_IsActive");

        builder.HasIndex(t => new { t.AgencyId, t.Role })
            .HasDatabaseName("IX_TrafficManagers_AgencyId_Role");

        builder.HasOne(t => t.Agency)
            .WithMany(a => a.TrafficManagers)
            .HasForeignKey(t => t.AgencyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_TrafficManagers_Agencies");

        builder.HasMany(t => t.PlatformAccounts)
            .WithOne(p => p.TrafficManager)
            .HasForeignKey(p => p.TrafficManagerId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_PlatformAccounts_TrafficManagers");

        builder.HasMany(t => t.ClientAssignments)
            .WithOne(ca => ca.TrafficManager)
            .HasForeignKey(ca => ca.TrafficManagerId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ClientAssignments_TrafficManagers");

        builder.Navigation(t => t.PlatformAccounts)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .AutoInclude(false);

        builder.Navigation(t => t.ClientAssignments)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .AutoInclude(false);
    }
}
