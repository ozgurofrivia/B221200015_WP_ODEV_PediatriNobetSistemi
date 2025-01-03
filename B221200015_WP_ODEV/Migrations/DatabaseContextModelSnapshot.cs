﻿// <auto-generated />
using System;
using B221200015_WP_ODEV.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace B221200015_WP_ODEV.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("B221200015_WP_ODEV.Models.AcilDurum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Konu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Saat")
                        .HasColumnType("time");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AcilDurumlar");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Asistan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BolumId")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BolumId");

                    b.ToTable("Asistanlar");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Bolum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BolumAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BosYatakSayisi")
                        .HasColumnType("int");

                    b.Property<int?>("HastaSayisi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Bolumler");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Hoca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BolumId")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BolumId");

                    b.ToTable("Hocalar");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.HocaMusaitlik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HocaId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Saat")
                        .HasColumnType("time");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HocaId");

                    b.ToTable("HocaMusaitlikler");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Nobet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AsistanId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("BaslamaSaati")
                        .HasColumnType("time");

                    b.Property<DateTime>("BaslamaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("BitisSaati")
                        .HasColumnType("time");

                    b.Property<DateTime>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("BolumId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AsistanId");

                    b.HasIndex("BolumId");

                    b.ToTable("Nobetler");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Randevu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AsistanId")
                        .HasColumnType("int");

                    b.Property<int>("HocaId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Saat")
                        .HasColumnType("time");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AsistanId");

                    b.HasIndex("HocaId");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("Hasta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BolumId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CikisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Durum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Yas")
                        .HasColumnType("int");

                    b.Property<string>("YatisDurumu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("YatisTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BolumId");

                    b.ToTable("Hastalar");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Asistan", b =>
                {
                    b.HasOne("B221200015_WP_ODEV.Models.Bolum", "Bolum")
                        .WithMany("Asistanlar")
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Bolum");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Hoca", b =>
                {
                    b.HasOne("B221200015_WP_ODEV.Models.Bolum", "Bolum")
                        .WithMany("Hocalar")
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Bolum");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.HocaMusaitlik", b =>
                {
                    b.HasOne("B221200015_WP_ODEV.Models.Hoca", "Hoca")
                        .WithMany("HocaMusaitlikler")
                        .HasForeignKey("HocaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hoca");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Nobet", b =>
                {
                    b.HasOne("B221200015_WP_ODEV.Models.Asistan", "Asistan")
                        .WithMany("Nobetler")
                        .HasForeignKey("AsistanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("B221200015_WP_ODEV.Models.Bolum", "Bolum")
                        .WithMany("Nobetler")
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asistan");

                    b.Navigation("Bolum");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Randevu", b =>
                {
                    b.HasOne("B221200015_WP_ODEV.Models.Asistan", "Asistan")
                        .WithMany("Randevular")
                        .HasForeignKey("AsistanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("B221200015_WP_ODEV.Models.Hoca", "Hoca")
                        .WithMany("Randevular")
                        .HasForeignKey("HocaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asistan");

                    b.Navigation("Hoca");
                });

            modelBuilder.Entity("Hasta", b =>
                {
                    b.HasOne("B221200015_WP_ODEV.Models.Bolum", "Bolum")
                        .WithMany("Hastalar")
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bolum");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Asistan", b =>
                {
                    b.Navigation("Nobetler");

                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Bolum", b =>
                {
                    b.Navigation("Asistanlar");

                    b.Navigation("Hastalar");

                    b.Navigation("Hocalar");

                    b.Navigation("Nobetler");
                });

            modelBuilder.Entity("B221200015_WP_ODEV.Models.Hoca", b =>
                {
                    b.Navigation("HocaMusaitlikler");

                    b.Navigation("Randevular");
                });
#pragma warning restore 612, 618
        }
    }
}
