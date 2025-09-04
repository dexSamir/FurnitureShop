using FurnitureShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureShop.DAL.Configurations;

public class CategoryConfiguration :  IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id); 
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasOne(x => x.ParentCategory)
            .WithMany(x => x.Subcategories)
            .HasForeignKey(x=> x.ParentCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(c => c.CreatedTime)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(c => c.UpdatedTime)
            .IsRequired(false)
            .HasColumnType("timestamp with time zone");

        
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.ParentCategoryId);
        builder.HasIndex(x => x.IsPrimary);
    }
}