using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileAi.Data.Migrations
{
    public partial class fixAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserStories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletedPoints",
                table: "Sprints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TotalCompletedPoints",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "CompletedPoints",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "FinishedAt",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TotalCompletedPoints",
                table: "Projects");
        }
    }
}
