using Microsoft.EntityFrameworkCore;
using Scoreboard.Domain.Models;

namespace Scoreboard.Data.Context;

public class ScoreboardDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Domain.Models.Stream> Streams { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StreamCourses> StreamCourses { get; set; }
    public DbSet<Assessment> Assessments { get; set; }
    public DbSet<StudentAssesment> StudentAssesments { get; set; }
    public DbSet<StudentTotalPoint> StudentTotalPoints { get; set; }

    public ScoreboardDbContext(DbContextOptions<ScoreboardDbContext> options): base(options) { 

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

        builder.Entity<StreamCourses>(b =>
        {
            b.HasKey(sc => new { sc.StreamId, sc.CourseId });
        });

        builder.Entity<StudentAssesment>(b =>
        {
            b.HasKey(sa => new { sa.StudentId, sa.AssessmentId });
        });

        builder.Entity<StudentTotalPoint>(b =>
        {
            b.HasOne(s => s.Student)
                .WithOne(s => s.StudentTotalPoint)
                .HasForeignKey<StudentTotalPoint>(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();
        });

        builder.Entity<Course>(b =>
        {
            b.HasMany(c => c.Streams)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasMany(b => b.Assesments)
                .WithOne(b => b.Course)
                .HasForeignKey(b => b.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }



}
