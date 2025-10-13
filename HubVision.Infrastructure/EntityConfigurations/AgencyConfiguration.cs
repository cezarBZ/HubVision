using HubVision.Domain.AggregatesModel.AgencyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class AgencyConfiguration : IEntityTypeConfiguration<Agency>
{
    public void Configure(EntityTypeBuilder<Agency> builder)
    {
        builder.ToTable("Agency", Data.ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.Slug)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.IsActive)
            .HasDefaultValue(true);

        builder.Property(a => a.Plan)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.Phone)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.LogoUrl);

        builder.Property(a => a.Website);

        builder.Property(a => a.CreatedAt);

        builder.Property(a => a.TrialEndsAt);

        builder.HasMany(a => a.TrafficManagers)
            .WithOne()
            .HasForeignKey(tm => tm.AgencyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Clients)
            .WithOne(c => c.Agency)
            .HasForeignKey(c => c.AgencyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => a.Slug)
        .IsUnique()
        .HasDatabaseName("idx_agency_code");

        builder.HasIndex(a => a.Name)
            .HasDatabaseName("idx_agency_name");
    }
}
