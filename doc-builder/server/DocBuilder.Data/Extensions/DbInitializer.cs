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
                    new DocCategory("Request")
                    {
                        Docs = new List<Doc>
                        {
                            new Doc("Account Request")
                            {
                                Items = new List<DocItem>
                                {
                                    new DocItem("Here is some nonsense.", DocItemType.Item) { Index = 0 },
                                    new DocItem("How questionable is this?", DocItemType.Question)
                                    {
                                        Index = 1,
                                        Answer = new DocAnswer("It is quite questionable.")
                                    },
                                    new DocItem("Make a selection.", DocItemType.Select)
                                    {
                                        Index = 2,
                                        IsDropdown = true,
                                        Options = new List<DocOption>
                                        {
                                            new DocOption("Air"),
                                            new DocOption("Water"),
                                            new DocOption("Earth"),
                                            new DocOption("Fire")
                                        }
                                    }
                                }
                            },
                            new Doc("Training Request")
                        }
                    },
                    new DocCategory("Assessment")
                    {
                        Docs = new List<Doc>
                        {
                            new Doc("Personal Assessment")
                            {
                                Description = "Determines candidate goal and value alignment"
                            }
                        }
                    },
                    new DocCategory("Research")
                };

                await db.DocCategories.AddRangeAsync(categories);
            }

            await db.SaveChangesAsync();
        }
    }
}