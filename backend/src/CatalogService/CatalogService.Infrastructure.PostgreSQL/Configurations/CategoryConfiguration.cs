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
            .HasConversion(
                id => id.Value,
                value => CategoryId.FromValue(value))
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .HasConversion(
                name => name.Value,
                value => CategoryName.Create(value).Value)
            .HasMaxLength(CategoryConstraints.MaxNameLength)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(CategoryConstraints.MaxDescriptionLength)
            .IsRequired(false);

        builder.Property(c => c.ParentCategoryId)
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
