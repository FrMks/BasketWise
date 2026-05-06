using CatalogService.Domain.Category;
using CatalogService.Domain.Category.ValueObjects;
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
            .HasMaxLength(Constants.Length100)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(Constants.Length500)
            .IsRequired(false);

        // 1. В базу: EF смотрит на свойство ParentCategoryId. Если оно null (категория верхнего
        //     уровня), он записывает null в колонку Postgres. Если там сидит объект CategoryId, он берет
        //     его внутреннее поле .Value (которое является Guid) и записывает его.
        // 2. Из базы: EF читает ячейку. Если там null, он кладет null в свойство C#. Если там лежит
        //     Guid, он вызывает ваш метод CategoryId.FromValue(guid), создает новый объект-ID и кладет
        //     его в свойство.
        builder.Property(c => c.ParentCategoryId)
            .HasColumnName("parent_category_id")
            .HasConversion(
                id => id == null ? (Guid?)null : id.Value,
                value => value == null ? null : CategoryId.FromValue(value.Value))
            .IsRequired(false);


        // Hierarchical relationship (Self-reference)
        builder.HasOne<Category>() // У категории есть ОДИН родитель
            .WithMany() // У этого родителя может быть МНОГО детей
            .HasForeignKey(c => c.ParentCategoryId) // Связь идет через колонку
            .OnDelete(DeleteBehavior.Restrict); // Restrict - нельзя удалить родителя, когда у него есть дети
        
        // Index for faster lookups by name
        builder.HasIndex(c => c.Name);
    }
}
