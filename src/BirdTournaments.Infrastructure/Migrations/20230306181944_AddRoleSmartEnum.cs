﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdTournaments.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class AddRoleSmartEnum : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AlterColumn<string>(
              name: "UserName",
              table: "Users",
              type: "nvarchar(100)",
              maxLength: 100,
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(max)");

          migrationBuilder.AlterColumn<string>(
              name: "Salt",
              table: "Users",
              type: "nvarchar(100)",
              maxLength: 100,
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(max)");

          migrationBuilder.AlterColumn<int>(
              name: "Role",
              table: "Users",
              type: "int",
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(max)");

          migrationBuilder.AlterColumn<string>(
              name: "Hash",
              table: "Users",
              type: "nvarchar(100)",
              maxLength: 100,
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(max)");

          migrationBuilder.AlterColumn<string>(
              name: "Email",
              table: "Users",
              type: "nvarchar(100)",
              maxLength: 100,
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(max)");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AlterColumn<string>(
              name: "UserName",
              table: "Users",
              type: "nvarchar(max)",
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(100)",
              oldMaxLength: 100);

          migrationBuilder.AlterColumn<string>(
              name: "Salt",
              table: "Users",
              type: "nvarchar(max)",
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(100)",
              oldMaxLength: 100);

          migrationBuilder.AlterColumn<string>(
              name: "Role",
              table: "Users",
              type: "nvarchar(max)",
              nullable: false,
              oldClrType: typeof(int),
              oldType: "int");

          migrationBuilder.AlterColumn<string>(
              name: "Hash",
              table: "Users",
              type: "nvarchar(max)",
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(100)",
              oldMaxLength: 100);

          migrationBuilder.AlterColumn<string>(
              name: "Email",
              table: "Users",
              type: "nvarchar(max)",
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(100)",
              oldMaxLength: 100);
      }
  }
