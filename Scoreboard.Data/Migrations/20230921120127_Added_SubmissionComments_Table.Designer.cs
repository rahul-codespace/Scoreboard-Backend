﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Scoreboard.Data.Context;

#nullable disable

namespace Scoreboard.Data.Migrations
{
    [DbContext(typeof(ScoreboardDbContext))]
    [Migration("20230921120127_Added_SubmissionComments_Table")]
    partial class Added_SubmissionComments_Table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Scoreboard.Domain.Models.Assessment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float?>("Point")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.Stream", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Streams");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.StreamCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("StreamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("StreamId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StreamCourses");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StreamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StreamId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.StudentAssessment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float?>("AchievedPoints")
                        .HasColumnType("real");

                    b.Property<int>("AssessmentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("StudentId", "AssessmentId");

                    b.HasIndex("AssessmentId");

                    b.ToTable("StudentAssesments");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.StudentTotalPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float?>("PercentageScore")
                        .HasColumnType("real");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<float?>("TotalAchievedPoints")
                        .HasColumnType("real");

                    b.Property<float?>("TotalPoints")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentTotalPoints");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.SubmissionComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "author_id");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "author_name");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("StudentAssessmentId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StudentAssessmentId");

                    b.HasIndex("StudentId");

                    b.ToTable("SubmissionComments");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.Assessment", b =>
                {
                    b.HasOne("Scoreboard.Domain.Models.Course", "Course")
                        .WithMany("Assessments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.StreamCourse", b =>
                {
                    b.HasOne("Scoreboard.Domain.Models.Course", "Course")
                        .WithMany("Streams")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scoreboard.Domain.Models.Stream", "Stream")
                        .WithMany("StreamCourses")
                        .HasForeignKey("StreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Stream");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.Student", b =>
                {
                    b.HasOne("Scoreboard.Domain.Models.Stream", "Stream")
                        .WithMany("Students")
                        .HasForeignKey("StreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stream");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.StudentAssessment", b =>
                {
                    b.HasOne("Scoreboard.Domain.Models.Assessment", "Assessment")
                        .WithMany("Students")
                        .HasForeignKey("AssessmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scoreboard.Domain.Models.Student", "Student")
                        .WithMany("StudentAssessments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assessment");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.StudentTotalPoint", b =>
                {
                    b.HasOne("Scoreboard.Domain.Models.Student", "Student")
                        .WithOne("StudentTotalPoint")
                        .HasForeignKey("Scoreboard.Domain.Models.StudentTotalPoint", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.SubmissionComment", b =>
                {
                    b.HasOne("Scoreboard.Domain.Models.StudentAssessment", "StudentAssessment")
                        .WithMany("SubmissionComments")
                        .HasForeignKey("StudentAssessmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scoreboard.Domain.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("StudentAssessment");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.Assessment", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.Course", b =>
                {
                    b.Navigation("Assessments");

                    b.Navigation("Streams");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.Stream", b =>
                {
                    b.Navigation("StreamCourses");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.Student", b =>
                {
                    b.Navigation("StudentAssessments");

                    b.Navigation("StudentTotalPoint");
                });

            modelBuilder.Entity("Scoreboard.Domain.Models.StudentAssessment", b =>
                {
                    b.Navigation("SubmissionComments");
                });
#pragma warning restore 612, 618
        }
    }
}
