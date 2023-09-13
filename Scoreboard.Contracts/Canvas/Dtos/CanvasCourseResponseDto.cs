using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Contracts.Canvas.Dtos;

public class CanvasCourseResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int AccountId { get; set; }
    public string Uuid { get; set; }
    public DateTime StartAt { get; set; }
    public int? GradingStandardId { get; set; }
    public bool? IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CourseCode { get; set; }
    public string DefaultView { get; set; }
    public int RootAccountId { get; set; }
    public int EnrollmentTermId { get; set; }
    public string License { get; set; }
    public string GradePassbackSetting { get; set; }
    public DateTime? EndAt { get; set; }
    public bool PublicSyllabus { get; set; }
    public bool PublicSyllabusToAuth { get; set; }
    public int StorageQuotaMb { get; set; }
    public bool IsPublicToAuthUsers { get; set; }
    public bool HideFinalGrades { get; set; }
    public bool ApplyAssignmentGroupWeights { get; set; }
    public CanvasCalendar Calendar { get; set; }
    public string TimeZone { get; set; }
    public bool Blueprint { get; set; }
    public string SisCourseId { get; set; }
    public string SisImportId { get; set; }
    public string IntegrationId { get; set; }
    public List<CanvasEnrollment> Enrollments { get; set; }
    public string WorkflowState { get; set; }
    public bool RestrictEnrollmentsToCourseDates { get; set; }
}

public class CanvasCalendar
{
    public string Ics { get; set; }
}

public class CanvasEnrollment
{
    public string Type { get; set; }
    public string Role { get; set; }
    public int RoleId { get; set; }
    public int UserId { get; set; }
    public string EnrollmentState { get; set; }
    public bool LimitPrivilegesToCourseSection { get; set; }
}
