using HubVision.Domain.AggregatesModel.AdSetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class AdSetConfiguration : IEntityTypeConfiguration<AdSet>
{
    public void Configure(EntityTypeBuilder<AdSet> builder)
    {
        builder.ToTable("AdSet", Data.ApplicationDbContext.DEFAULT_SCHEMA);
        builder.ToTable(a => a.HasCheckConstraint("CK_AdSets_DailyBudget_Positive",
            "\"daily_budget\" >= 0"));

        builder.ToTable(a => a.HasCheckConstraint("CK_AdSets_LifetimeBudget_Positive",
            "\"lifetime_budget\" IS NULL OR \"lifetime_budget\" >= 0"));

        builder.ToTable(a => a.HasCheckConstraint("CK_AdSets_EndTime_After_StartTime",
            "\"end_time\" IS NULL OR \"end_time\" >= \"start_time\""));

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(255)
            .HasComment("Nome do conjunto de anúncios");

        builder.Property(a => a.Status)
            .IsRequired()
            .HasMaxLength(50)
            .HasComment("Status: ACTIVE, PAUSED, DELETED, etc");

        builder.Property(a => a.DailyBudget)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2)
            .HasComment("Orçamento diário");

        builder.Property(a => a.LifetimeBudget)
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2)
            .HasComment("Orçamento vitalício (opcional)");

        builder.Property(a => a.StartTime)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasComment("Data/hora de início");

        builder.Property(a => a.EndTime)
            .HasColumnType("timestamp with time zone")
            .HasComment("Data/hora de término (opcional)");

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()")
            .HasComment("Data de criação no sistema");

        builder.Property(a => a.UpdatedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()")
            .HasComment("Data da última atualização");

        builder.Property(a => a.TargetingJson)
            .HasColumnType("jsonb")
            .HasComment("Configuração de segmentação em JSON");

        builder.Property(a => a.OptimizationGoal)
            .HasMaxLength(100)
            .HasComment("Meta de otimização (ex: CONVERSIONS, LINK_CLICKS)");

        builder.Property(a => a.BillingEvent)
            .HasMaxLength(100)
            .HasComment("Evento de cobrança (ex: IMPRESSIONS, LINK_CLICKS)");

        builder.HasIndex(a => new { a.AgencyId, a.CampaignId })
            .HasDatabaseName("IX_AdSets_AgencyId_CampaignId");

        builder.HasIndex(a => new { a.AgencyId, a.Status })
            .HasDatabaseName("IX_AdSets_AgencyId_Status");

        builder.HasIndex(a => a.CampaignId)
            .HasDatabaseName("IX_AdSets_CampaignId");

        builder.HasIndex(a => a.CreatedAt)
            .HasDatabaseName("IX_AdSets_CreatedAt");

        builder.HasIndex(a => new { a.StartTime, a.EndTime })
            .HasDatabaseName("IX_AdSets_StartTime_EndTime");

        builder.HasIndex(a => a.TargetingJson)
            .HasDatabaseName("IX_AdSets_TargetingJson")
            .HasMethod("gin"); 

        builder.HasOne(a => a.Agency)
            .WithMany()
            .HasForeignKey(a => a.AgencyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_AdSets_Agencies");

        builder.HasOne(a => a.Campaign)
            .WithMany(c => c.AdSets)
            .HasForeignKey(a => a.CampaignId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired()
            .HasConstraintName("FK_AdSets_Campaigns");

        builder.HasMany(a => a.Ads)
            .WithOne(ad => ad.AdSet)
            .HasForeignKey(ad => ad.AdSetId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Ads_AdSets");

        builder.Navigation(a => a.Ads)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .AutoInclude(false);

    }
}
