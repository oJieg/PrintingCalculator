﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using printing_calculator;

#nullable disable

namespace printing_calculator.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220921145123_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("printing_calculator.DataBase.ConsumablePrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DrumPrice1")
                        .HasColumnType("integer");

                    b.Property<int>("DrumPrice2")
                        .HasColumnType("integer");

                    b.Property<int>("DrumPrice3")
                        .HasColumnType("integer");

                    b.Property<int>("DrumPrice4")
                        .HasColumnType("integer");

                    b.Property<int>("TonerPrice")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ConsumablePrices");
                });

            modelBuilder.Entity("printing_calculator.DataBase.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ConsumablePriceId")
                        .HasColumnType("integer");

                    b.Property<int?>("CreasingPrice")
                        .HasColumnType("integer");

                    b.Property<int?>("CutPrice")
                        .HasColumnType("integer");

                    b.Property<int?>("DrillingPrice")
                        .HasColumnType("integer");

                    b.Property<int>("InputId")
                        .HasColumnType("integer");

                    b.Property<int?>("LaminationMarkup")
                        .HasColumnType("integer");

                    b.Property<int?>("LaminationPricesId")
                        .HasColumnType("integer");

                    b.Property<int?>("MarkupPaper")
                        .HasColumnType("integer");

                    b.Property<int?>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("PricePaperId")
                        .HasColumnType("integer");

                    b.Property<int?>("RoundingPrice")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ConsumablePriceId");

                    b.HasIndex("InputId");

                    b.HasIndex("LaminationPricesId");

                    b.HasIndex("PricePaperId");

                    b.ToTable("Historys");
                });

            modelBuilder.Entity("printing_calculator.DataBase.HistoryInput", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("CreasingAmount")
                        .HasColumnType("integer");

                    b.Property<int>("DrillingAmount")
                        .HasColumnType("integer");

                    b.Property<bool>("Duplex")
                        .HasColumnType("boolean");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("Kinds")
                        .HasColumnType("integer");

                    b.Property<int?>("LaminationId")
                        .HasColumnType("integer");

                    b.Property<int>("PaperId")
                        .HasColumnType("integer");

                    b.Property<bool>("RoundingAmount")
                        .HasColumnType("boolean");

                    b.Property<int>("Whidth")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LaminationId");

                    b.HasIndex("PaperId");

                    b.ToTable("HistoryInputs");
                });

            modelBuilder.Entity("printing_calculator.DataBase.Lamination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Laminations");
                });

            modelBuilder.Entity("printing_calculator.DataBase.LaminationPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("LaminationId")
                        .HasColumnType("integer");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("LaminationId");

                    b.ToTable("LaminationPrices");
                });

            modelBuilder.Entity("printing_calculator.DataBase.PaperCatalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SizeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SizeId");

                    b.ToTable("PaperCatalogs");
                });

            modelBuilder.Entity("printing_calculator.DataBase.PricePaper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CatalogId")
                        .HasColumnType("integer");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.ToTable("PricePapers");
                });

            modelBuilder.Entity("printing_calculator.DataBase.SizePaper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("NameSizePaper")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SizePaperHeight")
                        .HasColumnType("integer");

                    b.Property<int>("SizePaperWidth")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SizePapers");
                });

            modelBuilder.Entity("printing_calculator.DataBase.History", b =>
                {
                    b.HasOne("printing_calculator.DataBase.ConsumablePrice", "ConsumablePrice")
                        .WithMany()
                        .HasForeignKey("ConsumablePriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("printing_calculator.DataBase.HistoryInput", "Input")
                        .WithMany()
                        .HasForeignKey("InputId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("printing_calculator.DataBase.LaminationPrice", "LaminationPrices")
                        .WithMany()
                        .HasForeignKey("LaminationPricesId");

                    b.HasOne("printing_calculator.DataBase.PricePaper", "PricePaper")
                        .WithMany()
                        .HasForeignKey("PricePaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConsumablePrice");

                    b.Navigation("Input");

                    b.Navigation("LaminationPrices");

                    b.Navigation("PricePaper");
                });

            modelBuilder.Entity("printing_calculator.DataBase.HistoryInput", b =>
                {
                    b.HasOne("printing_calculator.DataBase.Lamination", "Lamination")
                        .WithMany()
                        .HasForeignKey("LaminationId");

                    b.HasOne("printing_calculator.DataBase.PaperCatalog", "Paper")
                        .WithMany()
                        .HasForeignKey("PaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lamination");

                    b.Navigation("Paper");
                });

            modelBuilder.Entity("printing_calculator.DataBase.LaminationPrice", b =>
                {
                    b.HasOne("printing_calculator.DataBase.Lamination", "Lamination")
                        .WithMany("Price")
                        .HasForeignKey("LaminationId");

                    b.Navigation("Lamination");
                });

            modelBuilder.Entity("printing_calculator.DataBase.PaperCatalog", b =>
                {
                    b.HasOne("printing_calculator.DataBase.SizePaper", "Size")
                        .WithMany()
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Size");
                });

            modelBuilder.Entity("printing_calculator.DataBase.PricePaper", b =>
                {
                    b.HasOne("printing_calculator.DataBase.PaperCatalog", "Catalog")
                        .WithMany("Prices")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");
                });

            modelBuilder.Entity("printing_calculator.DataBase.Lamination", b =>
                {
                    b.Navigation("Price");
                });

            modelBuilder.Entity("printing_calculator.DataBase.PaperCatalog", b =>
                {
                    b.Navigation("Prices");
                });
#pragma warning restore 612, 618
        }
    }
}
