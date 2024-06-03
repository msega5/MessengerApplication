﻿// <auto-generated />
using System;
using MessengerApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MessengerApplication.Migrations
{
    [DbContext(typeof(MessengerContext))]
    [Migration("20240603140113_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MessengerApplication.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("active");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<DateTime>("Registered")
                        .HasColumnType("datetime2")
                        .HasColumnName("registered");

                    b.HasKey("Id")
                        .HasName("user_id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[email] IS NOT NULL");

                    b.ToTable("users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
