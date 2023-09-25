using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;

namespace Scoreboard.Data
{
    public class ScorboardDataSeedContributor
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ScoreboardDbContext>();

                context.Database.EnsureCreated();

                if (!context.Streams.Any())
                {
                    context.Streams.AddRange(new List<Domain.Models.Stream>() {

                        new Domain.Models.Stream() {Name = "Backend (BE) - .NET"},
                        new Domain.Models.Stream() {Name = "Frontend (FE) - Angular"},
                        new Domain.Models.Stream() {Name = "Frontend (FE) - React"},
                        new Domain.Models.Stream() {Name = "Backend (BE) – Node"},
                        new Domain.Models.Stream() {Name = "Mobile - iOS (Flutter)"},
                        new Domain.Models.Stream() {Name = "Mobile - iOS"},
                    });
                    context.SaveChanges();
                }
                if (!context.Students.Any())
                {
                    context.Students.AddRange(new List<Student>()
                    {
                        new Student() { Id = 75, Name = "alisha", StreamId =  3 },
                        new Student() { Id = 76, Name = "Archie", StreamId =  6 },
                        new Student() { Id = 100, Name = "Asha Patel", StreamId =  6 },
                        new Student() { Id = 208, Name = "Akhilesh Chaurasiya", StreamId =  2 },
                        new Student() { Id = 297, Name = "Rahul Kumar", StreamId =  1 },
                        new Student() { Id = 315, Name = "ashiyana", StreamId =  2 },
                        new Student() { Id = 365, Name = "Sudipto", StreamId =  2 },
                        new Student() { Id = 381, Name = "anuja", StreamId =  4 },
                        new Student() { Id = 304, Name = "Mayank Vishwakarma", StreamId = 2 }
                    });
                    context.SaveChanges();
                }
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<IdentityRole<int>>()
                    {
                        new IdentityRole<int>() {Name = "HR", ConcurrencyStamp = "1", NormalizedName = "HR"},
                        new IdentityRole<int>() {Name = "TL", ConcurrencyStamp = "2", NormalizedName = "TL"},
                        new IdentityRole<int>() {Name = "Mentor", ConcurrencyStamp = "3", NormalizedName = "MENTOR"},
                        new IdentityRole<int>() {Name = "Student", ConcurrencyStamp = "4", NormalizedName = "STUDENT"}
                    });
                    context.SaveChanges();
                }

                if(!context.Users.Any())
                {
                    var hasher = new PasswordHasher<ScoreboardUser>();
                    context.Users.AddRange(new List<ScoreboardUser>()
                    {
                        new ScoreboardUser() {
                            Name = "Divyangi", 
                            UserName = "divyangi@promactinfo.com",
                            Email = "divyangi@promactinfo.com",
                            NormalizedEmail = "DIVYANGI@PROMACTINFO.COM",
                            LockoutEnabled = true,
                            PasswordHash = hasher.HashPassword(null, "Divyangi@2023"),
                        },
                        new ScoreboardUser() {
                            Name = "Dipa",
                            UserName = "dipa@promactinfo.com",
                            Email = "dipa@promactinfo.com",
                            NormalizedEmail = "DIPA@PROMACTINFO.COM",
                            LockoutEnabled = true,
                            PasswordHash = hasher.HashPassword(null, "Dipa@2023"),
                        },
                        new ScoreboardUser() {
                            Name = "Firoja",
                            UserName = "firoja@promactinfo.com",
                            Email = "firoja@promactinfo.com",
                            NormalizedEmail = "FIROJA@PROMACTINFO.COM",
                            LockoutEnabled = true,
                            PasswordHash = hasher.HashPassword(null, "Firoja@2023"),
                        },
                        new ScoreboardUser()
                        {
                            Name = "Rahul",
                            UserName = "rahul@promactinfo.com",
                            Email = "rahul@promactinfo.com",
                            NormalizedEmail = "RAHUL@PROMACTINFO.COM",
                            LockoutEnabled = true,
                            PasswordHash = hasher.HashPassword(null, "Rahul@2023"),
                        }
                    });
                    context.SaveChanges();
                }

                if(!context.UserRoles.Any())
                {
                    var divyangi = await context.Users.FirstOrDefaultAsync(u => u.Email == "divyangi@promactinfo.com");
                    var dipa = await context.Users.FirstOrDefaultAsync(u => u.Email == "dipa@promactinfo.com");
                    var firoja = await context.Users.FirstOrDefaultAsync(u => u.Email == "firoja@promactinfo.com");
                    var rahul = await context.Users.FirstOrDefaultAsync(u => u.Email == "rahul@promactinfo.com");

                    var hr = await context.Roles.FirstOrDefaultAsync(r => r.Name == "HR");
                    var tl = await context.Roles.FirstOrDefaultAsync(r => r.Name == "TL");
                    var mentor = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Mentor");
                    var student = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Student");

                    context.UserRoles.AddRange(new List<IdentityUserRole<int>>()
                    {

                        new IdentityUserRole<int>() {UserId = divyangi.Id, RoleId = hr.Id},
                        new IdentityUserRole<int>() {UserId = dipa.Id, RoleId = tl.Id},
                        new IdentityUserRole<int>() {UserId = firoja.Id, RoleId = mentor.Id},
                        new IdentityUserRole<int>() {UserId = rahul.Id, RoleId = student.Id}
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
