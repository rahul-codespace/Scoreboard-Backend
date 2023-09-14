using Microsoft.EntityFrameworkCore;
using Scoreboard.Data;
using Scoreboard.Data.Context;
using Scoreboard.Repository.Assessments;
using Scoreboard.Repository.Courses;
using Scoreboard.Repository.StudentAssessments;
using Scoreboard.Repository.Students;
using Scoreboard.Repository.StudentTotalPoints;
using Scoreboard.Service.Canvas;
using Scoreboard.Service.Canvas.Students;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var _configuration = builder.Configuration;

builder.Services.AddDbContext<ScoreboardDbContext>(options =>
        options.UseNpgsql(_configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string AppDbContext not found.")
    ));
builder.Services.AddScoped<CanvasAppServices>();
builder.Services.AddScoped<StudentAppServices>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAssessmentRepository, AssessmentRepository>();
builder.Services.AddScoped<IStudentAssessmentRepository, StudentAssessmentRepository>();
builder.Services.AddScoped<IStudentTotalPointRepository, StudentTotalPointRepository>();


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
