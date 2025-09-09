using FurnitureShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureShop.DAL.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.PublicId)
            .HasDefaultValueSql("gen_random_uuid()")
            .IsRequired();
        
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasMaxLength(1000);
        
        builder.Property(x => x.SellPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.CostPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(x => x.Discount)
            .HasDefaultValue(0);

        builder.Property(x => x.Quantity)
            .HasDefaultValue(0)
            .IsRequired();

        builder.HasMany(x => x.Images)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(c => c.CreatedTime)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(c => c.UpdatedTime)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");
        
        
        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.PublicId).IsUnique();
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.CreatedTime);
        builder.HasIndex(x => x.SellPrice);
        builder.HasIndex(x => x.Discount);
        builder.HasIndex(x => x.CategoryId);
    }
}