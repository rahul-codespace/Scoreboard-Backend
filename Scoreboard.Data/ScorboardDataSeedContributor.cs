using Microsoft.AspNetCore.Builder;
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

            }
        }
    }
}
