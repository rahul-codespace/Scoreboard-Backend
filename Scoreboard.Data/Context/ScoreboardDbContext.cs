using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scoreboard.Domain.Models;

namespace Scoreboard.Data.Context;

public class ScoreboardDbContext : IdentityDbContext<ScoreboardUser, IdentityRole<int>, int>
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Domain.Models.Stream> Streams { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StreamCourse> StreamCourses { get; set; }
    public DbSet<Assessment> Assessments { get; set; }
    public DbSet<StudentAssessment> StudentAssessments { get; set; }
    public DbSet<StudentTotalPoint> StudentTotalPoints { get; set; }
    public DbSet<SubmissionComment> SubmissionComments { get; set; }
    public DbSet<ScoreboardUser> Users { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }

    public ScoreboardDbContext(DbContextOptions<ScoreboardDbContext> options): base(options) {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Student>(b =>
        {
            b.HasOne(s => s.Stream)
                .WithMany(s => s.Students)
                .HasForeignKey(s => s.StreamId)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();
        });

        builder.Entity<StreamCourse>(b =>
        {
            b.HasKey(sc => sc.Id);
            b.HasAlternateKey(sc => new { sc.StreamId, sc.CourseId});
            b.Property(sc => sc.Id).ValueGeneratedOnAdd();
        });

        builder.Entity<StudentAssessment>(b =>
        {
            b.HasKey(sa => sa.Id);
            b.HasKey(sa => new { sa.StudentId, sa.AssessmentId});
            b.Property(sa => sa.Id).ValueGeneratedOnAdd();
        });

        builder.Entity<SubmissionComment>(b =>
        {
            b.HasKey(sc => sc.Id);
            b.HasOne(sc => sc.StudentAssessment)
                .WithMany(sa => sa.SubmissionComments)
                .HasForeignKey(sa => new {sa.StudentId, sa.AssessmentId})
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<StudentTotalPoint>(b =>
        {
            b.HasOne(s => s.Student)
                .WithOne(s => s.StudentTotalPoint)
                .HasForeignKey<StudentTotalPoint>(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Course>(b =>
        {
            b.HasMany(c => c.Streams)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasMany(b => b.Assessments)
                .WithOne(b => b.Course)
                .HasForeignKey(b => b.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Feedback>(b =>
        {
            b.HasKey(f => f.Id);
            b.HasOne(b => b.Student)
                .WithMany(b => b.Feedbacks)
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
