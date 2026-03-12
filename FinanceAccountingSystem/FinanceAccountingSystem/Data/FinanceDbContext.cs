using System;
using System.Collections.Generic;
using FinanceAccountingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccountingSystem.Data;

public partial class FinanceDbContext : DbContext
{
    public FinanceDbContext()
    {
    }

    public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=FinanceAccountingDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2AFB5010CA6F");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.PostCode).HasMaxLength(20);
            entity.Property(e => e.Street).HasMaxLength(200);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8536E07E4");

            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Customers__Addre__15502E78");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAB5BC4B0667");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Invoices__Custom__1920BF5C");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Invoices__Suppli__182C9B23");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666B48FEF5D40");

            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.TaxId).HasMaxLength(50);

            entity.HasOne(d => d.Address).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Suppliers__Addre__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
