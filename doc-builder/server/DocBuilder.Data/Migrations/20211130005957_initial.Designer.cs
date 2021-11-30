﻿// <auto-generated />
using DocBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DocBuilder.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211130005957_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId")
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

                    b.Property<int>("DocId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocId");

                    b.ToTable("DocItem", (string)null);

                    b.HasDiscriminator<string>("Type").HasValue("doc-item");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SelectId")
                        .HasColumnType("int");

                    b.Property<bool>("Selected")
                        .HasColumnType("bit");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SelectId");

                    b.ToTable("DocOption", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDoc", b =>
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

                    b.ToTable("TDoc", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDocItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<int>("TDocId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TDocId");

                    b.ToTable("TDocItem", (string)null);

                    b.HasDiscriminator<string>("Type").HasValue("tdoc-item");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDocOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SelectId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SelectId");

                    b.ToTable("TDocOption", (string)null);
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocInfo", b =>
                {
                    b.HasBaseType("DocBuilder.Data.Entities.DocItem");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("doc-info");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocQuestion", b =>
                {
                    b.HasBaseType("DocBuilder.Data.Entities.DocItem");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DocQuestion_Value");

                    b.HasDiscriminator().HasValue("doc-question");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocSelect", b =>
                {
                    b.HasBaseType("DocBuilder.Data.Entities.DocItem");

                    b.Property<bool>("AllowMultiple")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDropdown")
                        .HasColumnType("bit");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DocSelect_Value");

                    b.HasDiscriminator().HasValue("doc-select");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDocInfo", b =>
                {
                    b.HasBaseType("DocBuilder.Data.Entities.TDocItem");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("tdoc-info");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDocQuestion", b =>
                {
                    b.HasBaseType("DocBuilder.Data.Entities.TDocItem");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TDocQuestion_Value");

                    b.HasDiscriminator().HasValue("tdoc-question");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDocSelect", b =>
                {
                    b.HasBaseType("DocBuilder.Data.Entities.TDocItem");

                    b.Property<bool>("AllowMultiple")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDropdown")
                        .HasColumnType("bit");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TDocSelect_Value");

                    b.HasDiscriminator().HasValue("tdoc-select");
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
                    b.HasOne("DocBuilder.Data.Entities.DocQuestion", "Question")
                        .WithOne("Answer")
                        .HasForeignKey("DocBuilder.Data.Entities.DocAnswer", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
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

            modelBuilder.Entity("DocBuilder.Data.Entities.DocOption", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.DocSelect", "Select")
                        .WithMany("Options")
                        .HasForeignKey("SelectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Select");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDoc", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.DocCategory", "Category")
                        .WithMany("TDocs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDocItem", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.TDoc", "TDoc")
                        .WithMany("Items")
                        .HasForeignKey("TDocId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TDoc");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDocOption", b =>
                {
                    b.HasOne("DocBuilder.Data.Entities.TDocSelect", "Select")
                        .WithMany("Options")
                        .HasForeignKey("SelectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Select");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.Doc", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocCategory", b =>
                {
                    b.Navigation("Docs");

                    b.Navigation("TDocs");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDoc", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocQuestion", b =>
                {
                    b.Navigation("Answer");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.DocSelect", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("DocBuilder.Data.Entities.TDocSelect", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
