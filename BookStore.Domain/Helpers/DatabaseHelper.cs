using BookStore.Domain.Data;
using BookStore.Domain.Models;
using System;
using System.Linq;

namespace BookStore.Domain.Helpers
{
    public static class DatabaseHelper
    {
        public static void SeedDatabase(ApplicationDbContext context)
        {
            AddAuthors(context);
            AddBooks(context);
        }

        private static void AddBooks(ApplicationDbContext context)
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

                    context.Books.Add(book);
                    context.SaveChanges();
                }
            }
        }

        private static void AddAuthors(ApplicationDbContext context)
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

                    context.Authors.Add(author);
                    context.SaveChanges();
                }
            }
        }
    }
}