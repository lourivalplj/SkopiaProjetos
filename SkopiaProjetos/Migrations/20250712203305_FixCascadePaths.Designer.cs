﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkopiaProjetos.Data;

#nullable disable

namespace SkopiaProjetosAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250712203305_FixCascadePaths")]
    partial class FixCascadePaths
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SkopiaProjetos.Models.ComentarioTarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<int>("TarefaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TarefaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ComentariosTarefa");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.HistoricoTarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<int>("TarefaId")
                        .HasColumnType("int");

                    b.Property<string>("TipoAlteracao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<string>("ValorAntigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValorNovo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TarefaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("HistoricosTarefa");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.Tarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Funcao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.ComentarioTarefa", b =>
                {
                    b.HasOne("SkopiaProjetos.Models.Tarefa", "Tarefa")
                        .WithMany("Comentarios")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkopiaProjetos.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tarefa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.HistoricoTarefa", b =>
                {
                    b.HasOne("SkopiaProjetos.Models.Tarefa", "Tarefa")
                        .WithMany("Historico")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkopiaProjetos.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tarefa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.Projeto", b =>
                {
                    b.HasOne("SkopiaProjetos.Models.Usuario", "Usuario")
                        .WithMany("Projetos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.Tarefa", b =>
                {
                    b.HasOne("SkopiaProjetos.Models.Projeto", "Projeto")
                        .WithMany("Tarefas")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Projeto");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.Projeto", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.Tarefa", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Historico");
                });

            modelBuilder.Entity("SkopiaProjetos.Models.Usuario", b =>
                {
                    b.Navigation("Projetos");
                });
#pragma warning restore 612, 618
        }
    }
}
