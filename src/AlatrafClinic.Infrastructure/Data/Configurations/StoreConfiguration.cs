using AlatrafClinic.Domain.Inventory.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlatrafClinic.Infrastructure.Data.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(255);

        // Audit properties
        builder.Property(x => x.CreatedAtUtc).IsRequired();
        builder.Property(x => x.CreatedBy).HasMaxLength(200);
        builder.Property(x => x.LastModifiedUtc);
        builder.Property(x => x.LastModifiedBy).HasMaxLength(200);

        // FIXED: StoreItemUnits is NOT an owned type – it's a true entity
        builder.HasMany(x => x.StoreItemUnits)
               .WithOne(x => x.Store)
               .HasForeignKey(x => x.StoreId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.StoreItemUnits)
               .AutoInclude(false);
    }
}
