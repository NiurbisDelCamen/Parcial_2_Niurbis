﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parcial2.DAL;

namespace Parcial2.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20200316232004_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("Parcial2.Entidades.LLamada", b =>
                {
                    b.Property<int>("LlamadaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.HasKey("LlamadaId");

                    b.ToTable("Llamadas");
                });

            modelBuilder.Entity("Parcial2.Entidades.LlamadaDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LlamadaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Problema")
                        .HasColumnType("TEXT");

                    b.Property<string>("Solucion")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LlamadaId");

                    b.ToTable("LlamadaDetalle");
                });

            modelBuilder.Entity("Parcial2.Entidades.LlamadaDetalle", b =>
                {
                    b.HasOne("Parcial2.Entidades.LLamada", null)
                        .WithMany("Llamadas")
                        .HasForeignKey("LlamadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
