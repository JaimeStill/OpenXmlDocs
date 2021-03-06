// <auto-generated />
using DocBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DocBuilder.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DocBuilder.Data.Entities.Doc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Doc", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DocItemId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocItemId")
                        .IsUnique();

                    b.ToTable("DocAnswer", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DocCategory", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AllowMultiple")
                        .HasColumnType("bit");

                    b.Property<int>("DocId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsDropdown")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocId");

                    b.ToTable("DocItem", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocItemT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AllowMultiple")
                        .HasColumnType("bit");

                    b.Property<int>("DocTId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsDropdown")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocTId");

                    b.ToTable("DocItemT", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DocItemId")
                        .HasColumnType("int");

                    b.Property<bool>("Selected")
                        .HasColumnType("bit");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocItemId");

                    b.ToTable("DocOption", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocOptionT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DocItemTId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocItemTId");

                    b.ToTable("DocOptionT", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("DocT", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.Doc", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.DocCategory", "Category")
                        .WithMany("Docs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocAnswer", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.DocItem", "DocItem")
                        .WithOne("Answer")
                        .HasForeignKey("DocBuilder.Data.Entities.DocAnswer", "DocItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocItem");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocItem", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.Doc", "Doc")
                        .WithMany("Items")
                        .HasForeignKey("DocId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doc");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocItemT", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.DocT", "DocT")
                        .WithMany("Items")
                        .HasForeignKey("DocTId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocT");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocOption", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.DocItem", "DocItem")
                        .WithMany("Options")
                        .HasForeignKey("DocItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocItem");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocOptionT", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.DocItemT", "DocItemT")
                        .WithMany("Options")
                        .HasForeignKey("DocItemTId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocItemT");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocT", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.DocCategory", "Category")
                        .WithMany("DocTs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.Doc", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocCategory", b =>
                {
                    b.Navigation("DocTs");

                    b.Navigation("Docs");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocItem", b =>
                {
                    b.Navigation("Answer");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocItemT", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocT", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
