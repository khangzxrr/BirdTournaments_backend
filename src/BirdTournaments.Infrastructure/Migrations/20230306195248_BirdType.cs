using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdTournaments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BirdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BirdType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Birds_BirdTypeId",
                table: "Birds",
                column: "BirdTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_BirdType_BirdTypeId",
                table: "Birds",
                column: "BirdTypeId",
                principalTable: "BirdType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_BirdType_BirdTypeId",
                table: "Birds");

            migrationBuilder.DropTable(
                name: "BirdType");

            migrationBuilder.DropIndex(
                name: "IX_Birds_BirdTypeId",
                table: "Birds");
        }
    }
}
