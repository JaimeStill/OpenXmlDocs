using DocBuilder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocBuilder.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Doc> Docs => Set<Doc>();
        public DbSet<DocAnswer> DocAnswers => Set<DocAnswer>();
        public DbSet<DocCategory> DocCategories => Set<DocCategory>();
        public DbSet<DocItem> DocItems => Set<DocItem>();
        public DbSet<DocOption> DocOptions => Set<DocOption>();
        public DbSet<TDoc> TDocs => Set<TDoc>();
        public DbSet<TDocItem> TDocItems => Set<TDocItem>();
        public DbSet<TDocOption> TDocOptions => Set<TDocOption>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Doc>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Docs)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder
                .Entity<DocAnswer>()
                .HasOne(x => x.DocItem)
                .WithOne(x => x.Answer)
                .HasForeignKey<DocAnswer>(x => x.DocItemId);

            modelBuilder
                .Entity<DocItem>()
                .HasOne(x => x.Doc)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.DocId);

            modelBuilder
                .Entity<DocItem>()
                .Property(x => x.Type)
                .HasConversion(
                    x => x.ToString(),
                    x => (DocItemType)Enum.Parse(typeof(DocItemType), x)
                );

            modelBuilder
                .Entity<DocOption>()
                .HasOne(x => x.DocItem)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.DocItemId);

            modelBuilder
                .Entity<TDoc>()
                .HasOne(x => x.Category)
                .WithMany(x => x.TDocs)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder
                .Entity<TDocItem>()
                .HasOne(x => x.TDoc)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.TDocId);

            modelBuilder
                .Entity<TDocItem>()
                .Property(x => x.Type)
                .HasConversion(
                    x => x.ToString(),
                    x => (DocItemType)Enum.Parse(typeof(DocItemType), x)
                );

            modelBuilder
                .Entity<TDocOption>()
                .HasOne(x => x.TDocItem)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.TDocItemId);

            modelBuilder
                .Model
                .GetEntityTypes()
                .Where(x => x.BaseType == null)
                .ToList()
                .ForEach(x =>
                {
                    modelBuilder
                        .Entity(x.Name)
                        .ToTable(x.Name.Split('.').Last());
                });
        }
    }
}