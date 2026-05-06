using CatalogService.Domain.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.PostgreSQL.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => CategoryId.FromValue(value))
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasConversion(
                name => name.Value,
                value => CategoryName.Create(value).Value)
            .HasMaxLength(CategoryConstraints.Length100)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(CategoryConstraints.Length500)
            .IsRequired(false);

        builder.Property(c => c.ParentCategoryId)
            .HasColumnName("parent_category_id")
            .HasConversion(
                id => id == null ? (Guid?)null : id.Value,
                value => value == null ? null : CategoryId.FromValue(value.Value))
            .IsRequired(false);


        // Hierarchical relationship (Self-reference)
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Index for faster lookups by name
        builder.HasIndex(c => c.Name);
    }
}
