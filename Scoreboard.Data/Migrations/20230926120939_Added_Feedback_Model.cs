using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Scoreboard.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Feedback_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssesments_Assessments_AssessmentId",
                table: "StudentAssesments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssesments_Students_StudentId",
                table: "StudentAssesments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionComments_StudentAssesments_StudentId_AssessmentId",
                table: "SubmissionComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments");

            migrationBuilder.RenameTable(
                name: "StudentAssesments",
                newName: "StudentAssessments");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAssesments_AssessmentId",
                table: "StudentAssessments",
                newName: "IX_StudentAssessments_AssessmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAssessments",
                table: "StudentAssessments",
                columns: new[] { "StudentId", "AssessmentId" });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reviewer = table.Column<string>(type: "text", nullable: false),
                    ReviewerName = table.Column<string>(type: "text", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    FeedBackPoints = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_StudentId",
                table: "Feedbacks",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssessments_Assessments_AssessmentId",
                table: "StudentAssessments",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssessments_Students_StudentId",
                table: "StudentAssessments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionComments_StudentAssessments_StudentId_AssessmentId",
                table: "SubmissionComments",
                columns: new[] { "StudentId", "AssessmentId" },
                principalTable: "StudentAssessments",
                principalColumns: new[] { "StudentId", "AssessmentId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssessments_Assessments_AssessmentId",
                table: "StudentAssessments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssessments_Students_StudentId",
                table: "StudentAssessments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionComments_StudentAssessments_StudentId_AssessmentId",
                table: "SubmissionComments");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAssessments",
                table: "StudentAssessments");

            migrationBuilder.RenameTable(
                name: "StudentAssessments",
                newName: "StudentAssesments");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAssessments_AssessmentId",
                table: "StudentAssesments",
                newName: "IX_StudentAssesments_AssessmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments",
                columns: new[] { "StudentId", "AssessmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssesments_Assessments_AssessmentId",
                table: "StudentAssesments",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssesments_Students_StudentId",
                table: "StudentAssesments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionComments_StudentAssesments_StudentId_AssessmentId",
                table: "SubmissionComments",
                columns: new[] { "StudentId", "AssessmentId" },
                principalTable: "StudentAssesments",
                principalColumns: new[] { "StudentId", "AssessmentId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
