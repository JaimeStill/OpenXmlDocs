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
        public DbSet<DocInfo> DocInfos => Set<DocInfo>();
        public DbSet<DocItem> DocItems => Set<DocItem>();
        public DbSet<DocOption> DocOptions => Set<DocOption>();
        public DbSet<DocQuestion> DocQuestions => Set<DocQuestion>();
        public DbSet<DocSelect> DocSelects => Set<DocSelect>();
        public DbSet<TDoc> TDocs => Set<TDoc>();
        public DbSet<TDocInfo> TDocInfos => Set<TDocInfo>();
        public DbSet<TDocItem> TDocItems => Set<TDocItem>();
        public DbSet<TDocOption> TDocOptions => Set<TDocOption>();
        public DbSet<TDocQuestion> TDocQuestions => Set<TDocQuestion>();
        public DbSet<TDocSelect> TDocSelects => Set<TDocSelect>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Doc>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Docs)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder
                .Entity<DocAnswer>()
                .HasOne(x => x.Question)
                .WithOne(x => x.Answer)
                .HasForeignKey<DocAnswer>(x => x.QuestionId);

            modelBuilder
                .Entity<DocItem>()
                .HasOne(x => x.Doc)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.DocId);

            modelBuilder
                .Entity<DocItem>()
                .HasDiscriminator<string>(di => di.Type)
                .HasValue<DocItem>("doc-item")
                .HasValue<DocInfo>("doc-info")
                .HasValue<DocQuestion>("doc-question")
                .HasValue<DocSelect>("doc-select");

            modelBuilder
                .Entity<DocOption>()
                .HasOne(x => x.Select)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.SelectId);

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
                .HasDiscriminator<string>(tdi => tdi.Type)
                .HasValue<TDocItem>("tdoc-item")
                .HasValue<TDocInfo>("tdoc-info")
                .HasValue<TDocQuestion>("tdoc-question")
                .HasValue<TDocSelect>("tdoc-select");

            modelBuilder
                .Entity<TDocOption>()
                .HasOne(x => x.Select)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.SelectId);

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