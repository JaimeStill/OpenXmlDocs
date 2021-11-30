using DocBuilder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocBuilder.Data.Extensions
{
    public static class DbInitializer
    {
        public static async Task Initialize(this AppDbContext db)
        {
            Console.WriteLine("Initializing database");
            await db.Database.MigrateAsync();
            Console.WriteLine("Database initialized");

            // Seed data here
            if (!await db.DocCategories.AnyAsync())
            {
                Console.WriteLine("Seeding DocCategories...");
                var categories = new List<DocCategory>()
                {
                    new DocCategory("Request"),
                    new DocCategory("Assessment"),
                    new DocCategory("Research")
                };

                await db.DocCategories.AddRangeAsync(categories);
            }

            await db.SaveChangesAsync();
        }
    }
}