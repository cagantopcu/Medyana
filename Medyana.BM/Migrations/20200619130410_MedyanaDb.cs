using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medyana.BM.Migrations
{
    public partial class MedyanaDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Medyana");

            migrationBuilder.CreateTable(
                name: "Clinic",
                schema: "Medyana",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                schema: "Medyana",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    SupplyDate = table.Column<DateTime>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    UsageRate = table.Column<decimal>(nullable: false),
                    ClinicId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalSchema: "Medyana",
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ClinicId",
                schema: "Medyana",
                table: "Equipment",
                column: "ClinicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipment",
                schema: "Medyana");

            migrationBuilder.DropTable(
                name: "Clinic",
                schema: "Medyana");
        }
    }
}
