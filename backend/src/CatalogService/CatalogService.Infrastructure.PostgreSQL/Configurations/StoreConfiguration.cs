using CatalogService.Domain.Category;
using CatalogService.Domain.Chain;
using CatalogService.Domain.Chain.ValueObjects;
using CatalogService.Domain.Store;
using CatalogService.Domain.Store.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.PostgreSQL.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("stores");

        builder.HasKey(s => s.Id)
            .HasName("pk_stores");

         builder.Property(s => s.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => StoreId.FromValue(value))
            .ValueGeneratedNever();

        builder.Property(s => s.Name)
            .HasColumnName("name")
            .HasConversion(
                name => name.Value,
                value => StoreName.Create(value).Value)
            .HasMaxLength(Constants.Length100)
            .IsRequired();

        builder.Property(s => s.ChainId)
            .HasColumnName("chain_id")
            .HasConversion(
                chainId => chainId.Value,
                value => ChainId.FromValue(value))
            .IsRequired();

        builder.OwnsOne(s => s.Location, locationBuilder =>
        {
            locationBuilder.Property(l => l.Address)
                .HasColumnName("location_address")
                .HasMaxLength(Constants.Length250)
                .IsRequired();

            locationBuilder.Property(l => l.City)
                .HasColumnName("location_city")
                .HasMaxLength(Constants.Length100)
                .IsRequired();

            locationBuilder.Property(l => l.Region)
                .HasColumnName("location_region")
                .HasMaxLength(Constants.Length100)
                .IsRequired();

            locationBuilder.Property(l => l.Latitude)
                .HasColumnName("location_latitude")
                .HasPrecision(18, 10)
                .IsRequired();

            locationBuilder.Property(l => l.Longitude)
                .HasColumnName("location_longitude")
                .HasPrecision(18, 10)
                .IsRequired();
        });

        builder.HasOne<Chain>()
            .WithMany()
            .HasForeignKey(s => s.ChainId)
            .OnDelete(DeleteBehavior.Restrict);

        // ИНДЕКСЫ
        builder.HasIndex(s => s.Name)
            .HasDatabaseName("ix_stores_name");

        builder.HasIndex(s => s.ChainId)
            .HasDatabaseName("ix_stores_chain_id");
        }
        }