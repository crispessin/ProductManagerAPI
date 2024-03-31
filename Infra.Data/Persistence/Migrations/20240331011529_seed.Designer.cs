﻿// <auto-generated />
using Infra.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infra.Data.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240331011529_seed")]
    partial class seed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("Domain.Entites.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("idproduto")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1L)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("nome");

                    b.Property<double>("Price")
                        .HasColumnType("REAL")
                        .HasColumnName("preco");

                    b.Property<int>("Stock")
                        .HasColumnType("INTEGER")
                        .HasColumnName("estoque");

                    b.HasKey("ProductId");

                    b.ToTable("produto", (string)null);

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Name = "Teclado",
                            Price = 1.1000000000000001,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 2,
                            Name = "Monitor",
                            Price = 20.0,
                            Stock = 20
                        },
                        new
                        {
                            ProductId = 3,
                            Name = "Mouse",
                            Price = 30.0,
                            Stock = 30
                        },
                        new
                        {
                            ProductId = 4,
                            Name = "Fone",
                            Price = 30.0,
                            Stock = 40
                        },
                        new
                        {
                            ProductId = 5,
                            Name = "Carregador",
                            Price = 30.0,
                            Stock = 50
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
