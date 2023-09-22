using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Scoreboard.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_SubmissionComments_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StreamCourses",
                table: "StreamCourses");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentAssesments",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StreamCourses",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentAssesments_StudentId_AssessmentId",
                table: "StudentAssesments",
                columns: new[] { "StudentId", "AssessmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StreamCourses_StreamId_CourseId",
                table: "StreamCourses",
                columns: new[] { "StreamId", "CourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StreamCourses",
                table: "StreamCourses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SubmissionComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    StudentAssessmentId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    AuthorName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionComments_StudentAssesments_StudentAssessmentId",
                        column: x => x.StudentAssessmentId,
                        principalTable: "StudentAssesments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmissionComments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionComments_StudentAssessmentId",
                table: "SubmissionComments",
                column: "StudentAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionComments_StudentId",
                table: "SubmissionComments",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmissionComments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentAssesments_StudentId_AssessmentId",
                table: "StudentAssesments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StreamCourses_StreamId_CourseId",
                table: "StreamCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StreamCourses",
                table: "StreamCourses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentAssesments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StreamCourses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments",
                columns: new[] { "StudentId", "AssessmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StreamCourses",
                table: "StreamCourses",
                columns: new[] { "StreamId", "CourseId" });
        }
    }
}
