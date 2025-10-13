using HubVision.Domain.AggregatesModel.ClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class ClientAssignmentConfiguration : IEntityTypeConfiguration<ClientAssignment>
{
    public void Configure(EntityTypeBuilder<ClientAssignment> builder)
    {
        builder.ToTable("ClientAssignments");

        builder.HasKey(ca => ca.Id);

        builder.Property(ca => ca.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(ca => ca.AssignedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");

        builder.Property(ca => ca.AssignedBy)
            .IsRequired();

        builder.Property(ca => ca.RemovedAt)
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(ca => new { ca.ClientId, ca.TrafficManagerId })
            .HasDatabaseName("IX_ClientAssignments_ClientId_TrafficManagerId");

        builder.HasIndex(ca => new { ca.AgencyId, ca.ClientId })
            .HasDatabaseName("IX_ClientAssignments_AgencyId_ClientId");

        builder.HasIndex(ca => new { ca.AgencyId, ca.TrafficManagerId })
            .HasDatabaseName("IX_ClientAssignments_AgencyId_TrafficManagerId");

        builder.HasIndex(ca => new { ca.ClientId, ca.IsPrimary, ca.RemovedAt })
            .HasDatabaseName("IX_ClientAssignments_ClientId_IsPrimary_RemovedAt")
            .HasFilter("\"removed_at\" IS NULL");

        builder.HasIndex(ca => ca.AssignedAt)
            .HasDatabaseName("IX_ClientAssignments_AssignedAt");

        builder.HasOne(ca => ca.Agency)
            .WithMany()
            .HasForeignKey(ca => ca.AgencyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ClientAssignments_Agencies");

        builder.HasOne(ca => ca.Client)
            .WithMany(c => c.ClientAssignments)
            .HasForeignKey(ca => ca.ClientId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired()
            .HasConstraintName("FK_ClientAssignments_Clients");

        builder.HasOne(ca => ca.TrafficManager)
            .WithMany(t => t.ClientAssignments)
            .HasForeignKey(ca => ca.TrafficManagerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired()
            .HasConstraintName("FK_ClientAssignments_TrafficManagers");
    }
}
