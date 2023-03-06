using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdTournaments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RankUpcase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_ranks_RankId",
                table: "Birds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ranks",
                table: "ranks");

            migrationBuilder.RenameTable(
                name: "ranks",
                newName: "Ranks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ranks",
                table: "Ranks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_Ranks_RankId",
                table: "Birds",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_Ranks_RankId",
                table: "Birds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ranks",
                table: "Ranks");

            migrationBuilder.RenameTable(
                name: "Ranks",
                newName: "ranks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ranks",
                table: "ranks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_ranks_RankId",
                table: "Birds",
                column: "RankId",
                principalTable: "ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
