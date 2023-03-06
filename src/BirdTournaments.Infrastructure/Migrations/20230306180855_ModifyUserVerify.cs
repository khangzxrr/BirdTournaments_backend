using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdTournaments.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class ModifyUserVerify : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AlterColumn<int>(
              name: "Verify",
              table: "users",
              type: "int",
              nullable: false,
              oldClrType: typeof(bool),
              oldType: "bit");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AlterColumn<bool>(
              name: "Verify",
              table: "users",
              type: "bit",
              nullable: false,
              oldClrType: typeof(int),
              oldType: "int");
      }
  }
