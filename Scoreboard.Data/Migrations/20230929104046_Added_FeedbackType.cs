using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scoreboard.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_FeedbackType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedBackPoints",
                table: "Feedbacks",
                newName: "FeedbackPoints");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Feedbacks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FeedbackType",
                table: "Feedbacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "InCooperated",
                table: "Feedbacks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "FeedbackType",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "InCooperated",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "FeedbackPoints",
                table: "Feedbacks",
                newName: "FeedBackPoints");
        }
    }
}
