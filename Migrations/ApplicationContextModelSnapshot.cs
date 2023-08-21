﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using printing_calculator;

#nullable disable

namespace printing_calculator.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("printing_calculator.DataBase.СalculationHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("ConsumablePriceId")
                        .HasColumnType("integer");

                    b.Property<int?>("CreasingPrice")
                        .HasColumnType("integer");

                    b.Property<int?>("CutPrice")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DrillingPrice")
                        .HasColumnType("integer");

                    b.Property<int>("InputId")
                        .HasColumnType("integer");

                    b.Property<int?>("LaminationMarkup")
                        .HasColumnType("integer");

                    b.Property<float?>("LaminationPrices")
                        .HasColumnType("real");

                    b.Property<int?>("MarkupPaper")
                        .HasColumnType("integer");

                    b.Property<float>("PaperPrice")
                        .HasColumnType("real");

                    b.Property<int?>("Price")
                        .HasColumnType("integer");

                    b.Property<int?>("RoundingPrice")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ConsumablePriceId");

                    b.HasIndex("InputId");

                    b.ToTable("Histories");
                });

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

            modelBuilder.Entity("printing_calculator.DataBase.InputHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<List<string>>("CommonToAllMarkupName")
                        .HasColumnType("text[]");

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

                    b.ToTable("InputsHistories");
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

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Laminations");
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

                    b.Property<float>("Prices")
                        .HasColumnType("real");

                    b.Property<int>("SizeId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SizeId");

                    b.ToTable("PaperCatalogs");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.CommonToAllMarkup", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int>("Adjustmen")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PercentMarkup")
                        .HasColumnType("integer");

                    b.Property<int?>("SettingId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SettingId");

                    b.ToTable("CommonToAllMarkups");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.MachineSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdjustmenPrice")
                        .HasColumnType("integer");

                    b.Property<float>("ConsumableOther")
                        .HasColumnType("real");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameMachine")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SettingId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NameMachine")
                        .IsUnique();

                    b.HasIndex("SettingId");

                    b.ToTable("MachineSettings");

                    b.HasDiscriminator<string>("Discriminator").HasValue("MachineSetting");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.Markup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("MachineSettingId")
                        .HasColumnType("integer");

                    b.Property<int>("MarkupForThisPage")
                        .HasColumnType("integer");

                    b.Property<int>("Page")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MachineSettingId");

                    b.ToTable("Markups");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("printing_calculator.DataBase.SizePaper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SizePapers");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.PosMachinesSetting", b =>
                {
                    b.HasBaseType("printing_calculator.DataBase.setting.MachineSetting");

                    b.Property<float>("AddMoreHit")
                        .HasColumnType("real");

                    b.Property<int>("CountOfPapersInOneAdjustmentCut")
                        .HasColumnType("integer");

                    b.Property<int?>("SettingId1")
                        .HasColumnType("integer");

                    b.HasIndex("SettingId1");

                    b.HasDiscriminator().HasValue("PosMachinesSetting");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.PrintingMachineSetting", b =>
                {
                    b.HasBaseType("printing_calculator.DataBase.setting.MachineSetting");

                    b.Property<int>("Bleed")
                        .HasColumnType("integer");

                    b.Property<int>("ConsumableDye")
                        .HasColumnType("integer");

                    b.Property<int>("FieldForLabels")
                        .HasColumnType("integer");

                    b.Property<int>("MainConsumableForDrawing")
                        .HasColumnType("integer");

                    b.Property<int>("MaximumSizeLength")
                        .HasColumnType("integer");

                    b.Property<int>("MaximumSizeWidth")
                        .HasColumnType("integer");

                    b.Property<int?>("SettingId1")
                        .HasColumnType("integer")
                        .HasColumnName("PrintingMachineSetting_SettingId1");

                    b.Property<float>("WhiteFieldHeight")
                        .HasColumnType("real");

                    b.Property<float>("WhiteFieldWidth")
                        .HasColumnType("real");

                    b.HasIndex("SettingId1");

                    b.HasDiscriminator().HasValue("PrintingMachineSetting");
                });

            modelBuilder.Entity("printing_calculator.DataBase.СalculationHistory", b =>
                {
                    b.HasOne("printing_calculator.DataBase.ConsumablePrice", "ConsumablePrice")
                        .WithMany()
                        .HasForeignKey("ConsumablePriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("printing_calculator.DataBase.InputHistory", "Input")
                        .WithMany()
                        .HasForeignKey("InputId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConsumablePrice");

                    b.Navigation("Input");
                });

            modelBuilder.Entity("printing_calculator.DataBase.InputHistory", b =>
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

            modelBuilder.Entity("printing_calculator.DataBase.PaperCatalog", b =>
                {
                    b.HasOne("printing_calculator.DataBase.SizePaper", "Size")
                        .WithMany()
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Size");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.CommonToAllMarkup", b =>
                {
                    b.HasOne("printing_calculator.DataBase.setting.Setting", null)
                        .WithMany("CommonToAllMarkups")
                        .HasForeignKey("SettingId");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.MachineSetting", b =>
                {
                    b.HasOne("printing_calculator.DataBase.setting.Setting", null)
                        .WithMany("Machines")
                        .HasForeignKey("SettingId");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.Markup", b =>
                {
                    b.HasOne("printing_calculator.DataBase.setting.MachineSetting", null)
                        .WithMany("Markups")
                        .HasForeignKey("MachineSettingId");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.PosMachinesSetting", b =>
                {
                    b.HasOne("printing_calculator.DataBase.setting.Setting", null)
                        .WithMany("PosMachines")
                        .HasForeignKey("SettingId1");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.PrintingMachineSetting", b =>
                {
                    b.HasOne("printing_calculator.DataBase.setting.Setting", null)
                        .WithMany("PrintingsMachines")
                        .HasForeignKey("SettingId1");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.MachineSetting", b =>
                {
                    b.Navigation("Markups");
                });

            modelBuilder.Entity("printing_calculator.DataBase.setting.Setting", b =>
                {
                    b.Navigation("CommonToAllMarkups");

                    b.Navigation("Machines");

                    b.Navigation("PosMachines");

                    b.Navigation("PrintingsMachines");
                });
#pragma warning restore 612, 618
        }
    }
}
