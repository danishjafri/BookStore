using BookStore.Domain.Data;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Helpers
{
    public static class DatabaseHelper
    {
        public async static Task SeedDatabase
            (
                ApplicationDbContext context,
                UserManager<IdentityUser<int>> userManager,
                RoleManager<IdentityRole<int>> roleManager
            )
        {
            await AddAuthors(context);
            await AddBooks(context);

            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private async static Task AddBooks(ApplicationDbContext context)
        {
            if (!context.Books.Any())
            {
                for (int i = 1; i <= 3; i++)
                {
                    var book = new Book
                    {
                        Title = $"Book {i}",
                        Year = Convert.ToInt32($"200{i}"),
                        ISBN = $"ISBN-{i}",
                        Summary = $"Successful book with a lot of pages.",
                        Image = $"image-{i}",
                        Price = 3.00F,
                        AuthorId = i
                    };

                    await context.Books.AddAsync(book);
                    await context.SaveChangesAsync();
                }
            }
        }

        private async static Task AddAuthors(ApplicationDbContext context)
        {
            if (!context.Authors.Any())
            {
                for (int i = 1; i <= 3; i++)
                {
                    var author = new Author
                    {
                        Firstname = "Author",
                        Lastname = $"No {i}",
                        Bio = $"Author working, Bio number {i}"
                    };

                    await context.Authors.AddAsync(author);
                    await context.SaveChangesAsync();
                }
            }
        }

        private async static Task SeedUsers(UserManager<IdentityUser<int>> userManager)
        {
            if (await userManager.FindByEmailAsync(UserHelper.Credentials.AdminEmail) == null)
            {
                var user = new IdentityUser<int>
                {
                    UserName = UserHelper.Credentials.AdminUserName,
                    Email = UserHelper.Credentials.AdminEmail
                };

                var result = await userManager.CreateAsync(user, UserHelper.Credentials.AdminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserHelper.Roles.Administrator);
                }
            }

            if (await userManager.FindByEmailAsync(UserHelper.Credentials.CustomerEmail) == null)
            {
                var user = new IdentityUser<int>
                {
                    UserName = UserHelper.Credentials.CustomerUserName,
                    Email = UserHelper.Credentials.CustomerEmail
                };

                var result = await userManager.CreateAsync(user, UserHelper.Credentials.CustomerPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserHelper.Roles.Customer);
                }
            }
        }
        private async static Task SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(UserHelper.Roles.Administrator))
            {
                var role = new IdentityRole<int> { Name = UserHelper.Roles.Administrator };
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync(UserHelper.Roles.Customer))
            {
                var role = new IdentityRole<int> { Name = UserHelper.Roles.Customer };
                await roleManager.CreateAsync(role);
            }
        }
    }
}