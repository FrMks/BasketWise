using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Infrastructure.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    parent_category_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_categories_categories_parent_category_id",
                        column: x => x.parent_category_id,
                        principalSchema: "catalog",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "chains",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    logo_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chains", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    brand = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    barcode = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    measurement_value = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    measurement_unit = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_categories_category_id",
                        column: x => x.category_id,
                        principalSchema: "catalog",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    chain_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    location_city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    location_region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    location_latitude = table.Column<double>(type: "double precision", precision: 18, scale: 10, nullable: false),
                    location_longitude = table.Column<double>(type: "double precision", precision: 18, scale: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stores", x => x.id);
                    table.ForeignKey(
                        name: "FK_stores_chains_chain_id",
                        column: x => x.chain_id,
                        principalSchema: "catalog",
                        principalTable: "chains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_name",
                schema: "catalog",
                table: "categories",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_categories_parent_category_id",
                schema: "catalog",
                table: "categories",
                column: "parent_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_chains_name",
                schema: "catalog",
                table: "chains",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_products_category_id",
                schema: "catalog",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_name",
                schema: "catalog",
                table: "products",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ux_products_barcode",
                schema: "catalog",
                table: "products",
                column: "barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stores_chain_id",
                schema: "catalog",
                table: "stores",
                column: "chain_id");

            migrationBuilder.CreateIndex(
                name: "ix_stores_name",
                schema: "catalog",
                table: "stores",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "stores",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "chains",
                schema: "catalog");
        }
    }
}
