using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.models
{
    public partial class Employee_dbContext : DbContext
    {
        public Employee_dbContext()
        {
        }

        public Employee_dbContext(DbContextOptions<Employee_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerTbl> CustomerTbls { get; set; } = null!;
        public virtual DbSet<EmployeePayTbl> EmployeePayTbls { get; set; } = null!;
        public virtual DbSet<EmployeeTbl> EmployeeTbls { get; set; } = null!;
        public virtual DbSet<OrdersTbl> OrdersTbls { get; set; } = null!;
        public virtual DbSet<ProductsTbl> ProductsTbls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-OP3HLHL;Database= Employee_db;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerTbl>(entity =>
            {
                entity.HasKey(e => e.CustId)
                    .HasName("PK__Customer__049E3A89F6BE593C");

                entity.ToTable("Customer_tbl");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.CustAddress)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CustCity)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CustFax)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CustPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmpId)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("EmpID");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.CustomerTbls)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Customer___EmpID__2B3F6F97");
            });

            modelBuilder.Entity<EmployeePayTbl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Employee_pay_tbl");

                entity.Property(e => e.DateHire).HasColumnType("datetime");

                entity.Property(e => e.DateLastRaise).HasColumnType("datetime");

                entity.Property(e => e.EmpId)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("EmpID");

                entity.HasOne(d => d.Emp)
                    .WithMany()
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Employee___EmpID__2C3393D0");
            });

            modelBuilder.Entity<EmployeeTbl>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__AF2DBA7954CCD8E4");

                entity.ToTable("Employee_tbl");

                entity.Property(e => e.EmpId)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("EmpID");

                entity.Property(e => e.Address)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrdersTbl>(entity =>
            {
                entity.HasKey(e => e.OrdNum)
                    .HasName("PK__Orders_t__4C38D72A497CEE27");

                entity.ToTable("Orders_tbl");

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.OrdDate).HasColumnType("datetime");

                entity.Property(e => e.ProdId).HasColumnName("ProdID");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.OrdersTbls)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK__Orders_tb__CustI__2D27B809");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.OrdersTbls)
                    .HasForeignKey(d => d.ProdId)
                    .HasConstraintName("FK__Orders_tb__ProdI__2E1BDC42");
            });

            modelBuilder.Entity<ProductsTbl>(entity =>
            {
                entity.HasKey(e => e.ProdId)
                    .HasName("PK__Products__042785C5BFEBFB9B");

                entity.ToTable("Products_tbl");

                entity.Property(e => e.ProdId).HasColumnName("ProdID");

                entity.Property(e => e.ProdDesc)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
