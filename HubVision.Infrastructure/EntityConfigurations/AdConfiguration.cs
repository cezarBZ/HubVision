using HubVision.Domain.AggregatesModel.AdAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class AdConfiguration : IEntityTypeConfiguration<Ad>
{
    public void Configure(EntityTypeBuilder<Ad> builder)
    {
        builder.ToTable(nameof(Ad), Data.ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.CreativeId)
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("ID do criativo na plataforma");

        builder.Property(a => a.ImageUrl)
            .HasMaxLength(1000);

        builder.Property(a => a.VideoUrl)
            .HasMaxLength(1000);

        builder.Property(a => a.AdText)
            .HasMaxLength(5000);

        builder.Property(a => a.Headline)
            .HasMaxLength(500);

        builder.Property(a => a.Description)
            .HasMaxLength(2000);

        builder.Property(a => a.CallToAction)
            .HasMaxLength(100);

        builder.Property(a => a.LinkUrl)
            .HasMaxLength(2000);

        builder.Property(a => a.AdSetId)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(a => a.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.HasIndex(a => new { a.AgencyId, a.AdSetId })
            .HasDatabaseName("IX_Ad_AgencyId_AdSetId");

        builder.HasIndex(a => new { a.AgencyId, a.Status })
            .HasDatabaseName("IX_Ad_AgencyId_Status");

        builder.HasIndex(a => a.CreativeId)
            .HasDatabaseName("IX_Ad_CreativeId");

        builder.HasIndex(a => new { a.AdSetId, a.Status })
            .HasDatabaseName("IX_Ad_AdSetId_Status");

        builder.HasIndex(a => a.CreatedAt)
            .HasDatabaseName("IX_Ad_CreatedAt");

        builder.HasOne(a => a.AdSet)
            .WithMany(ads => ads.Ads)
            .HasForeignKey(a => a.AdSetId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Ad_AdSets");

        builder.HasOne(a => a.Agency)
            .WithMany()
            .HasForeignKey(a => a.AgencyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Ad_Agencies");
    }
}