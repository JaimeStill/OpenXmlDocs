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

            if (!await db.DocCategories.AnyAsync())
            {
                Console.WriteLine("Seeding DocCategories...");
                Console.WriteLine("Seeding Docs...");
                Console.WriteLine("Seeding DocItems...");
                Console.WriteLine("Seeding DocOptions...");
                Console.WriteLine("Seeding DocAnswers...");
                Console.WriteLine("Seeding DocTs...");
                Console.WriteLine("Seeding DocItemTs...");
                Console.WriteLine("Seeding DocOptionTs...");

                var categories = new List<DocCategory>()
                {
                    new DocCategory("Request")
                    {
                        DocTs = new List<DocT>
                        {
                            new DocT("Account Request")
                            {
                                Items = new List<DocItemT>
                                {
                                    new DocItemT("Here is some nonsense.", DocItemType.Item) { Index = 0 },
                                    new DocItemT("How questionable is this?", DocItemType.Question) { Index = 1 },
                                    new DocItemT("Make a selection.", DocItemType.Select)
                                    {
                                        Index = 2,
                                        Options = new List<DocOptionT>
                                        {
                                            new DocOptionT("Air"),
                                            new DocOptionT("Water"),
                                            new DocOptionT("Earth"),
                                            new DocOptionT("Fire")
                                        }
                                    }
                                }
                            },
                            new DocT("Training Request")
                        },
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
                        DocTs = new List<DocT>
                        {
                            new DocT("Perosnal Assessment")
                            {
                                Description = "Determines candidate goal and value alignment."
                            }
                        },
                        Docs = new List<Doc>
                        {
                            new Doc("Personal Assessment")
                            {
                                Description = "Determines candidate goal and value alignment."
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