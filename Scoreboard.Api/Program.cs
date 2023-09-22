using Microsoft.EntityFrameworkCore;
using Scoreboard.Data;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Assessments;
using Scoreboard.Repository.Courses;
using Scoreboard.Repository.StreamCourses;
using Scoreboard.Repository.Streams;
using Scoreboard.Repository.StudentAssessments;
using Scoreboard.Repository.Students;
using Scoreboard.Repository.StudentTotalPoints;
using Scoreboard.Repository.SubmissionComments;
using Scoreboard.Service.BackgroundWork;
using Scoreboard.Service.Canvas;
using Scoreboard.Service.Canvas.Students;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var _configuration = builder.Configuration;

builder.Services.AddDbContext<ScoreboardDbContext>(options =>
        options.UseNpgsql(_configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string AppDbContext not found.")
    ));
builder.Services.AddScoped<IStudentAppServices, StudentAppServices>();
builder.Services.AddScoped<IGetStudentDataServices, GetStudentDataServices>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAssessmentRepository, AssessmentRepository>();
builder.Services.AddScoped<IStudentAssessmentRepository, StudentAssessmentRepository>();
builder.Services.AddScoped<IStudentTotalPointRepository, StudentTotalPointRepository>();
builder.Services.AddScoped<IStreamCoursesRepository, StreamCoursesRepository>();
builder.Services.AddScoped<ISubmissionCommentRepository, SubmissionCommentRepository>();
builder.Services.AddScoped<IStreamRepository, StreamRepository>();
//builder.Services.AddHostedService<ScoreboardBackgroundServices>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ScorboardDataSeedContributor.Seed(app);

app.Run();
