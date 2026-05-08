using CatalogService.Domain.Category;
using CatalogService.Domain.Category.ValueObjects;
using CatalogService.Domain.Product;
using CatalogService.Domain.Product.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.PostgreSQL.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id)
            .HasName("pk_products");

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => ProductId.FromValue(value))
            .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasConversion(
                name => name.Value,
                value => ProductName.Create(value).Value)
            .HasMaxLength(Constants.Length250)
            .IsRequired();
        
        builder.Property(p => p.Brand)
            .HasColumnName("brand")
            .HasConversion(
                brand => brand.Value,
                value => Brand.Create(value).Value)
            .HasMaxLength(Constants.Length100)
            .IsRequired();

        builder.Property(p => p.Barcode)
            .HasColumnName("barcode")
            .HasConversion(
                barcode => barcode.Value,
                value => Barcode.Create(value).Value)
            .HasMaxLength(Constants.Length13)
            .IsRequired();

        builder.OwnsOne(p => p.Measurement, measurementBuilder =>
        {
            measurementBuilder.Property(m => m.Value)
                .HasColumnName("measurement_value")
                .HasPrecision(18, 4) // 18 всего цифр, количество цифр после запятой
                .IsRequired();

            measurementBuilder.Property(m => m.Unit)
                .HasColumnName("measurement_unit")
                .IsRequired();
        });

        builder.Property(p => p.CategoryId)
            .HasColumnName("category_id")
            .HasConversion(
                categoryId => categoryId.Value,
                value => CategoryId.FromValue(value))
            .IsRequired();

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(Constants.Length500)
            .IsRequired(false);

        builder
            .HasIndex(p => p.Barcode)
            .IsUnique()
            .HasDatabaseName("ux_products_barcode");

        builder
            .HasIndex(p => p.Name)
            .HasDatabaseName("ix_products_name");
    }
}