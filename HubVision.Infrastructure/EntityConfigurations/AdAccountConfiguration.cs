using HubVision.Domain.AggregatesModel.AdAccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class AdAccountConfiguration : IEntityTypeConfiguration<AdAccount>
{
    public void Configure(EntityTypeBuilder<AdAccount> builder)
    {
        builder.ToTable("AdAccount", Data.ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AdAccountId)
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("ID da conta de anúncios na plataforma (ex: act_123456)");

        builder.Property(a => a.AccountName)
            .IsRequired()
            .HasMaxLength(255)
            .HasComment("Nome da conta de anúncios");

        builder.Property(a => a.Currency)
            .IsRequired()
            .HasMaxLength(3)
            .HasDefaultValue("BRL")
            .HasComment("Moeda da conta (ISO 4217)");

        builder.Property(a => a.TimeZone)
            .HasMaxLength(100)
            .HasDefaultValue("America/Sao_Paulo")
            .HasComment("Fuso horário da conta");

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasDefaultValue(AdAccountStatus.Active)
            .HasSentinel(0)
            .HasComment("Status da conta: 1=Active, 2=Disabled, 3=Suspended, 4=Closed");

        builder.Property(a => a.IsActive)
            .IsRequired()
            .HasDefaultValue(true)
            .HasComment("Se a conta está ativa no sistema");

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()")
            .HasComment("Data de criação no sistema");

        builder.Property(a => a.LastSyncedAt)
            .HasComment("Data da última sincronização com a plataforma");

        builder.HasIndex(a => new { a.AgencyId, a.AdAccountId })
            .IsUnique()
            .HasDatabaseName("IX_AdAccounts_AgencyId_AdAccountId");

        builder.HasIndex(a => new { a.AgencyId, a.ClientId })
            .HasDatabaseName("IX_AdAccounts_AgencyId_ClientId");

        builder.HasIndex(a => a.PlatformAccountId)
            .HasDatabaseName("IX_AdAccounts_PlatformAccountId");

        builder.HasIndex(a => new { a.AgencyId, a.IsActive })
            .HasDatabaseName("IX_AdAccounts_AgencyId_IsActive");

        builder.HasOne(a => a.Agency)
            .WithMany()
            .HasForeignKey(a => a.AgencyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_AdAccounts_Agencies");

        builder.HasOne(a => a.PlatformAccount)
            .WithMany(p => p.AdAccounts)
            .HasForeignKey(a => a.PlatformAccountId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired()
            .HasConstraintName("FK_AdAccounts_PlatformAccounts");

        builder.HasOne(a => a.Client)
            .WithMany(c => c.AdAccounts)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false)
            .HasConstraintName("FK_AdAccounts_Clients");

        builder.HasMany(a => a.Campaigns)
            .WithOne(c => c.AdAccount)
            .HasForeignKey(c => c.AdAccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Campaigns_AdAccounts");

        builder.HasMany(a => a.AdAccountAccesses)
            .WithOne(aa => aa.AdAccount)
            .HasForeignKey(aa => aa.AdAccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_AdAccountAccesses_AdAccounts");

        builder.Navigation(a => a.Campaigns)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.Navigation(a => a.AdAccountAccesses)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

    }
}
