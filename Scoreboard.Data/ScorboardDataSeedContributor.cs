﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scoreboard.Data.Context;

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

            }
        }
    }
}