using BookStore.API.Contracts;
using BookStore.Domain.Data;
using BookStore.Domain.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BookStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILoggerService>();

            try
            {
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                logger.LogInfo("Database migration successful.");

                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser<int>>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                DatabaseHelper.SeedDatabase(context, userManager, roleManager).Wait();
                logger.LogInfo("Database seed successful.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error encountered while creating and seeding the database. {ex.Message}");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}