using HubVision.Domain.AggregatesModel.ClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubVision.Infrastructure.EntityConfigurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable(nameof(Client), Data.ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(x => x.Id);

        builder.Property(a => a.Name)
        .HasMaxLength(200)
        .IsRequired(); 
        
        builder.Property(a => a.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.PasswordHash)
            .IsRequired();

        builder.Property(a => a.IsActive)
        .HasDefaultValue(true);

        builder.Property(a => a.Phone)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.PhotoUrl).IsRequired();

        builder.Property(a => a.Company).IsRequired();

        builder.Property(a => a.IsActive)
        .HasDefaultValue(true);

        builder.Property(a => a.CreatedAt);

        builder.Property(a => a.LastLoginAt);
    }
}
