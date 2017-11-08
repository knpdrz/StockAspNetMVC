﻿// <auto-generated />
using EmbeddedStock.Data;
using EmbeddedStock.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EmbeddedStock.Migrations
{
    [DbContext(typeof(StockContext))]
    [Migration("20171107182852_one")]
    partial class one
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmbeddedStock.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EmbeddedStock.Models.Component", b =>
                {
                    b.Property<long>("ComponentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdminComment");

                    b.Property<int>("ComponentNumber");

                    b.Property<long>("ComponentTypeID");

                    b.Property<long?>("CurrentLoanInformationId");

                    b.Property<string>("SerialNo");

                    b.Property<int>("Status");

                    b.Property<string>("UserComment");

                    b.HasKey("ComponentID");

                    b.HasIndex("ComponentTypeID");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("EmbeddedStock.Models.ComponentType", b =>
                {
                    b.Property<long>("ComponentTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdminComment");

                    b.Property<string>("ComponentInfo");

                    b.Property<string>("ComponentName");

                    b.Property<string>("Datasheet");

                    b.Property<long?>("ImageESImageId");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Location");

                    b.Property<string>("Manufacturer");

                    b.Property<int>("Status");

                    b.Property<string>("WikiLink");

                    b.HasKey("ComponentTypeID");

                    b.HasIndex("ImageESImageId");

                    b.ToTable("ComponentType");
                });

            modelBuilder.Entity("EmbeddedStock.Models.ComponentTypeCategory", b =>
                {
                    b.Property<int>("CategoryID");

                    b.Property<long>("ComponentTypeID");

                    b.HasKey("CategoryID", "ComponentTypeID");

                    b.HasIndex("ComponentTypeID");

                    b.ToTable("ComponentTypeCategories");
                });

            modelBuilder.Entity("EmbeddedStock.Models.ESImage", b =>
                {
                    b.Property<long>("ESImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ImageData");

                    b.Property<string>("ImageMimeType")
                        .HasMaxLength(128);

                    b.Property<byte[]>("Thumbnail");

                    b.HasKey("ESImageId");

                    b.ToTable("ESImage");
                });

            modelBuilder.Entity("EmbeddedStock.Models.Component", b =>
                {
                    b.HasOne("EmbeddedStock.Models.ComponentType", "ComponentType")
                        .WithMany("Components")
                        .HasForeignKey("ComponentTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmbeddedStock.Models.ComponentType", b =>
                {
                    b.HasOne("EmbeddedStock.Models.ESImage", "Image")
                        .WithMany()
                        .HasForeignKey("ImageESImageId");
                });

            modelBuilder.Entity("EmbeddedStock.Models.ComponentTypeCategory", b =>
                {
                    b.HasOne("EmbeddedStock.Models.Category", "Category")
                        .WithMany("ComponentTypeCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmbeddedStock.Models.ComponentType", "ComponentType")
                        .WithMany("ComponentTypeCategories")
                        .HasForeignKey("ComponentTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
