using FurnitureShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureShop.DAL.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.ImageUrl)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .IsRequired(false); 

        builder.Property(pi => pi.IsPrimary)
            .HasDefaultValue(false);

        builder.Property(pi => pi.AltText)
            .HasColumnType("varchar(150)")
            .IsRequired(false); 

        builder.Property(pi => pi.ProductId)
            .IsRequired();

        builder.HasOne(pi => pi.Product)
            .WithMany(p => p.Images) 
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(pi => pi.CreatedTime)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(pi => pi.UpdatedTime)
            .HasColumnType("timestamp with time zone")
            .IsRequired(false);

        builder.Property(pi => pi.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(pi => pi.IsUpdated)
            .HasDefaultValue(false);

        builder.HasIndex(pi => pi.ProductId);
        builder.HasIndex(pi => pi.IsPrimary);
        builder.HasIndex(pi => pi.IsDeleted); 
        builder.HasIndex(pi => pi.IsUpdated);
        builder.HasIndex(pi => pi.CreatedTime);
    }
}