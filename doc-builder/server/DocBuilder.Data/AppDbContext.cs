using DocBuilder.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DocBuilder.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Doc> Docs => Set<Doc>();
        public DbSet<DocT> DocTs => Set<DocT>();
        public DbSet<DocAnswer> DocAnswers => Set<DocAnswer>();
        public DbSet<DocCategory> DocCategories => Set<DocCategory>();
        public DbSet<DocItem> DocItems => Set<DocItem>();
        public DbSet<DocItemT> DocItemTs => Set<DocItemT>();
        public DbSet<DocOption> DocOptions => Set<DocOption>();
        public DbSet<DocOptionT> DocOptionTs => Set<DocOptionT>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Doc>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Docs)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder
                .Entity<DocT>()
                .HasOne(x => x.Category)
                .WithMany(x => x.DocTs)
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
                .Entity<DocItemT>()
                .HasOne(x => x.DocT)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.DocTId);

            modelBuilder
                .Entity<DocItemT>()
                .Property(x => x.Type)
                .HasConversion(new EnumToStringConverter<DocItemType>());

            modelBuilder
                .Entity<DocOption>()
                .HasOne(x => x.DocItem)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.DocItemId);

            modelBuilder
                .Entity<DocOptionT>()
                .HasOne(x => x.DocItemT)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.DocItemTId);

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