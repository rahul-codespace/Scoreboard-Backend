using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scoreboard.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Relationship_SubmissionComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionComments_StudentAssesments_StudentAssessmentId",
                table: "SubmissionComments");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionComments_StudentAssessmentId",
                table: "SubmissionComments");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionComments_StudentId",
                table: "SubmissionComments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentAssesments_StudentId_AssessmentId",
                table: "StudentAssesments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments");

            migrationBuilder.RenameColumn(
                name: "StudentAssessmentId",
                table: "SubmissionComments",
                newName: "AssessmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments",
                columns: new[] { "StudentId", "AssessmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionComments_StudentId_AssessmentId",
                table: "SubmissionComments",
                columns: new[] { "StudentId", "AssessmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionComments_StudentAssesments_StudentId_AssessmentId",
                table: "SubmissionComments",
                columns: new[] { "StudentId", "AssessmentId" },
                principalTable: "StudentAssesments",
                principalColumns: new[] { "StudentId", "AssessmentId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionComments_StudentAssesments_StudentId_AssessmentId",
                table: "SubmissionComments");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionComments_StudentId_AssessmentId",
                table: "SubmissionComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments");

            migrationBuilder.RenameColumn(
                name: "AssessmentId",
                table: "SubmissionComments",
                newName: "StudentAssessmentId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentAssesments_StudentId_AssessmentId",
                table: "StudentAssesments",
                columns: new[] { "StudentId", "AssessmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAssesments",
                table: "StudentAssesments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionComments_StudentAssessmentId",
                table: "SubmissionComments",
                column: "StudentAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionComments_StudentId",
                table: "SubmissionComments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionComments_StudentAssesments_StudentAssessmentId",
                table: "SubmissionComments",
                column: "StudentAssessmentId",
                principalTable: "StudentAssesments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
