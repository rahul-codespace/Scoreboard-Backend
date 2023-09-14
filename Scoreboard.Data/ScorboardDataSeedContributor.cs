using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scoreboard.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Data
{
    public class ScorboardDataSeedContributor
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ScoreboardDbContext>();

                context.Database.EnsureCreated();

                if (!context.Streams.Any())
                {
                    context.Streams.AddRange(new List<Domain.Models.Stream>() {

                        new Domain.Models.Stream() {Name = "Backend (asp.net)"},
                        new Domain.Models.Stream() {Name = "Frontend (Angular)"},
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
