﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MojeMiasto.Data;

#nullable disable

namespace MojeMiasto.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20221013112113_quests_hired_id")]
    partial class quests_hired_id
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MojeMiasto.Models.Chat", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("user1_id")
                        .HasColumnType("int");

                    b.Property<int>("user2_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("chats");
                });

            modelBuilder.Entity("MojeMiasto.Models.City", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("cities");
                });

            modelBuilder.Entity("MojeMiasto.Models.District", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("city_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("districts");
                });

            modelBuilder.Entity("MojeMiasto.Models.Message", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("chat_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("MojeMiasto.Models.Quest", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("city_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("create_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("district_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("end_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("hired_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("reward")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("quests");
                });

            modelBuilder.Entity("MojeMiasto.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("city_id")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("points")
                        .HasColumnType("int");

                    b.Property<int>("region_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}