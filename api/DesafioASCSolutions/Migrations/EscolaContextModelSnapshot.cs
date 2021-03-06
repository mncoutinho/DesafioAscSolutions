﻿// <auto-generated />
using System;
using DesafioASCSolutions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioASCSolutions.Migrations
{
    [DbContext(typeof(EscolaContext))]
    partial class EscolaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DesafioASCSolutions.Models.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdTurma")
                        .HasColumnType("int");

                    b.Property<double?>("Media")
                        .HasColumnType("float");

                    b.Property<double?>("MediaFinal")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Nota1")
                        .HasColumnType("float");

                    b.Property<double?>("Nota2")
                        .HasColumnType("float");

                    b.Property<double?>("Nota3")
                        .HasColumnType("float");

                    b.Property<string>("StatusAluno")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("DesafioASCSolutions.Models.Premio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlunoId")
                        .HasColumnType("int");

                    b.Property<int>("IdAluno")
                        .HasColumnType("int");

                    b.Property<double>("ProvaPremio")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.ToTable("Premios");
                });

            modelBuilder.Entity("DesafioASCSolutions.Models.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("DesafioASCSolutions.Models.Premio", b =>
                {
                    b.HasOne("DesafioASCSolutions.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId");
                });
#pragma warning restore 612, 618
        }
    }
}
