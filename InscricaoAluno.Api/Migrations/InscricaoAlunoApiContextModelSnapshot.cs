﻿// <auto-generated />
using System;
using InscricaoAluno.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InscricaoAluno.Api.Migrations
{
    [DbContext(typeof(InscricaoAlunoApiContext))]
    partial class InscricaoAlunoApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InscricaoAluno.Api.Models.Aluno", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF");

                    b.Property<string>("Nome");

                    b.Property<string>("RG");

                    b.HasKey("id");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("InscricaoAluno.Api.Models.Curso", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao");

                    b.Property<string>("Nome");

                    b.Property<string>("Professor");

                    b.HasKey("id");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("InscricaoAluno.Api.Models.Inscricao", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Alunoid");

                    b.Property<int?>("Avaliacao");

                    b.Property<int?>("Cursoid");

                    b.Property<DateTime>("Inicio");

                    b.Property<DateTime?>("Termino");

                    b.Property<int>("idAluno");

                    b.Property<int>("idCurso");

                    b.HasKey("id");

                    b.HasIndex("Alunoid");

                    b.HasIndex("Cursoid");

                    b.ToTable("Inscricao");
                });

            modelBuilder.Entity("InscricaoAluno.Api.Models.Inscricao", b =>
                {
                    b.HasOne("InscricaoAluno.Api.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("Alunoid");

                    b.HasOne("InscricaoAluno.Api.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("Cursoid");
                });
#pragma warning restore 612, 618
        }
    }
}
