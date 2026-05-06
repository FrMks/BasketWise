using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.Domain.Category;
using CatalogService.Domain.Chain;
using CatalogService.Domain.Chain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.PostgreSQL.Configurations;

public class ChainConfiguration : IEntityTypeConfiguration<Chain>
{
    public void Configure(EntityTypeBuilder<Chain> builder)
    {
        builder.ToTable("chains");

        builder.HasKey(c => c.Id)
            .HasName("pk_chain");

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => ChainId.FromValue(value))
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasConversion(
                name => name.Value,
                value => ChainName.Create(value).Value)
            .HasMaxLength(Constants.Length100)
            .IsRequired();

        builder.Property(c => c.LogoUrl)
            .HasColumnName("logo_url");
    }
}