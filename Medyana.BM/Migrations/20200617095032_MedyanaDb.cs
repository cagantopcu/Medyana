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
                    Name = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clinic",
                schema: "Medyana");
        }
    }
}
