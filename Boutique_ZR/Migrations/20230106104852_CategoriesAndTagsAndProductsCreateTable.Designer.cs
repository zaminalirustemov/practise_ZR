﻿// <auto-generated />
using System;
using Boutique_ZR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Boutique_ZR.Migrations
{
    [DbContext(typeof(BoutiqueDbContext))]
    [Migration("20230106104852_CategoriesAndTagsAndProductsCreateTable")]
    partial class CategoriesAndTagsAndProductsCreateTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Boutique_ZR.Models.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Boutique_ZR.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<double>("CostPrice")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DiscountPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SKU")
                        .HasColumnType("int");

                    b.Property<double>("SalePrice")
                        .HasColumnType("float");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.Property<int?>("categoriesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TagsId");

                    b.HasIndex("categoriesId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Boutique_ZR.Models.Tags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Boutique_ZR.Models.Products", b =>
                {
                    b.HasOne("Boutique_ZR.Models.Tags", "tags")
                        .WithMany("Products")
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boutique_ZR.Models.Categories", "categories")
                        .WithMany("Products")
                        .HasForeignKey("categoriesId");

                    b.Navigation("categories");

                    b.Navigation("tags");
                });

            modelBuilder.Entity("Boutique_ZR.Models.Categories", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Boutique_ZR.Models.Tags", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
