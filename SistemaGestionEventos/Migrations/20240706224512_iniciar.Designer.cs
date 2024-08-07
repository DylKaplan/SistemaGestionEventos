﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaGestionEventos.Context;

#nullable disable

namespace SistemaGestionEventos.Migrations
{
    [DbContext(typeof(EventosDatabaseContext))]
    [Migration("20240706224512_iniciar")]
    partial class iniciar
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SistemaGestionEventos.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Institucion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCliente");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("SistemaGestionEventos.Models.Evento", b =>
                {
                    b.Property<int>("IdEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEvento"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Equipamiento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdLugar")
                        .HasColumnType("int");

                    b.Property<int>("Presupuesto")
                        .HasColumnType("int");

                    b.HasKey("IdEvento");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdLugar");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("SistemaGestionEventos.Models.EventoPersonal", b =>
                {
                    b.Property<int>("IdEventoPersonal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEventoPersonal"));

                    b.Property<int>("IdEvento")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonal")
                        .HasColumnType("int");

                    b.HasKey("IdEventoPersonal");

                    b.HasIndex("IdEvento");

                    b.HasIndex("IdPersonal");

                    b.ToTable("EventoPersonal");
                });

            modelBuilder.Entity("SistemaGestionEventos.Models.Lugar", b =>
                {
                    b.Property<int>("IdLugar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLugar"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("tieneEscaleras")
                        .HasColumnType("bit");

                    b.HasKey("IdLugar");

                    b.ToTable("Lugares");
                });

            modelBuilder.Entity("SistemaGestionEventos.Models.Personal", b =>
                {
                    b.Property<int>("IdPersonal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPersonal"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsPersonalFlete")
                        .HasColumnType("bit");

                    b.Property<bool>("EsPersonalTecnico")
                        .HasColumnType("bit");

                    b.Property<int>("Especializacion")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TamanioFlete")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPersonal");

                    b.ToTable("Personal");
                });

            modelBuilder.Entity("SistemaGestionEventos.Models.Evento", b =>
                {
                    b.HasOne("SistemaGestionEventos.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaGestionEventos.Models.Lugar", "Lugar")
                        .WithMany()
                        .HasForeignKey("IdLugar")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Lugar");
                });

            modelBuilder.Entity("SistemaGestionEventos.Models.EventoPersonal", b =>
                {
                    b.HasOne("SistemaGestionEventos.Models.Evento", "Evento")
                        .WithMany()
                        .HasForeignKey("IdEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaGestionEventos.Models.Personal", "Personal")
                        .WithMany()
                        .HasForeignKey("IdPersonal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Personal");
                });
#pragma warning restore 612, 618
        }
    }
}
