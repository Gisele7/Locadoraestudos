﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Filme_Locadora
{
    public partial class VIDEO_LOCADORAContext : DbContext
    {
        public VIDEO_LOCADORAContext()
        {
        }

        public VIDEO_LOCADORAContext(DbContextOptions<VIDEO_LOCADORAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tbcategorias> Tbcategorias { get; set; }
        public virtual DbSet<Tbclientes> Tbclientes { get; set; }
        public virtual DbSet<Tbfilmes> Tbfilmes { get; set; }
        public virtual DbSet<Tbpedidos> Tbpedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=RES701136\\SQLEXPRESS;initial catalog=VIDEO_LOCADORA;integrated security=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbclientes>(entity =>
            {
                entity.HasKey(e => e.Documento)
                    .HasName("PK__TBClient__AF73706C4DA649C7");
            });

            modelBuilder.Entity<Tbfilmes>(entity =>
            {
                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Tbfilmes)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TBFilmes__IdCate__3E52440B");
            });

            modelBuilder.Entity<Tbpedidos>(entity =>
            {
                entity.HasOne(d => d.DocClienteNavigation)
                    .WithMany(p => p.Tbpedidos)
                    .HasForeignKey(d => d.DocCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TBPedidos__DocCl__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}