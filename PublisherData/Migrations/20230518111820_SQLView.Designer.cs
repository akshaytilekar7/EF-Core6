﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublisherData;

#nullable disable

namespace PublisherData.Migrations
{
    [DbContext(typeof(PubContext))]
    [Migration("20230518111820_SQLView")]
    partial class SQLView
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ArtistCover", b =>
                {
                    b.Property<int>("ArtistsArtistId")
                        .HasColumnType("int");

                    b.Property<int>("CoversCoverId")
                        .HasColumnType("int");

                    b.HasKey("ArtistsArtistId", "CoversCoverId");

                    b.HasIndex("CoversCoverId");

                    b.ToTable("ArtistCover");
                });

            modelBuilder.Entity("PublisherDomain.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistId"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistId");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("PublisherDomain.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("PublisherDomain.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("PublisherDomain.BookWithCoverView", b =>
                {
                    b.Property<string>("DesignIdeas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToView("BookWithCoverView");
                });

            modelBuilder.Entity("PublisherDomain.Cover", b =>
                {
                    b.Property<int>("CoverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CoverId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("DesignIdeas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DigitalOnly")
                        .HasColumnType("bit");

                    b.HasKey("CoverId");

                    b.HasIndex("BookId")
                        .IsUnique();

                    b.ToTable("Covers");
                });

            modelBuilder.Entity("ArtistCover", b =>
                {
                    b.HasOne("PublisherDomain.Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PublisherDomain.Cover", null)
                        .WithMany()
                        .HasForeignKey("CoversCoverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PublisherDomain.Book", b =>
                {
                    b.HasOne("PublisherDomain.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("PublisherDomain.Cover", b =>
                {
                    b.HasOne("PublisherDomain.Book", "Book")
                        .WithOne("Cover")
                        .HasForeignKey("PublisherDomain.Cover", "BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("PublisherDomain.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("PublisherDomain.Book", b =>
                {
                    b.Navigation("Cover")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
