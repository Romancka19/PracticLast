using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Практика1.Models;

public partial class PracticContext : DbContext
{
    private static PracticContext model = null;

    public PracticContext()
    {
    }

    public PracticContext(DbContextOptions<PracticContext> options)
        : base(options)
    {
    }

    public static PracticContext GetContext()
    {
        if(model == null)
        {
            model = new PracticContext();
        }
        return model;
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Practic;Username=postgres;Password=19982005");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("Accounts_pkey");

            entity.Property(e => e.IdEmployee)
                .ValueGeneratedNever()
                .HasColumnName("ID_Employee");

            entity.HasOne(d => d.IdEmployeeNavigation).WithOne(p => p.Account)
                .HasForeignKey<Account>(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Accounts_ID_Employee_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Clients_pkey");

            entity.Property(e => e.IdClient).HasColumnName("ID_Client");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("Employees_pkey");

            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
            entity.Property(e => e.Salary).HasColumnType("money");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("Orders_pkey");

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.IdClient).HasColumnName("ID_Client");
            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_ID_Client_fkey");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_ID_Employee_fkey");

            entity.HasMany(d => d.IdProducts).WithMany(p => p.IdOrders)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsOrder",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Products_Orders_ID_Product_fkey"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Products_Orders_ID_Order_fkey"),
                    j =>
                    {
                        j.HasKey("IdOrder", "IdProduct").HasName("Products_Orders_pkey");
                        j.ToTable("Products_Orders");
                        j.IndexerProperty<long>("IdOrder").HasColumnName("ID_Order");
                        j.IndexerProperty<long>("IdProduct").HasColumnName("ID_Product");
                    });

            entity.HasMany(d => d.IdServices).WithMany(p => p.IdOrders)
                .UsingEntity<Dictionary<string, object>>(
                    "ServicesOrder",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("IdService")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Services_Orders_ID_Service_fkey"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Services_Orders_ID_Order_fkey"),
                    j =>
                    {
                        j.HasKey("IdOrder", "IdService").HasName("Services_Orders_pkey");
                        j.ToTable("Services_Orders");
                        j.IndexerProperty<long>("IdOrder").HasColumnName("ID_Order");
                        j.IndexerProperty<long>("IdService").HasColumnName("ID_Service");
                    });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("Products_pkey");

            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");
            entity.Property(e => e.IdProvider).HasColumnName("ID_Provider");
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.IdProviderNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProvider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_ID_Provider_fkey");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.IdProvider).HasName("Providers_pkey");

            entity.Property(e => e.IdProvider).HasColumnName("ID_Provider");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview).HasName("Reviews_pkey");

            entity.Property(e => e.IdReview).HasColumnName("ID_Review");
            entity.Property(e => e.IdClient).HasColumnName("ID_Client");
            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reviews_ID_Client_fkey");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reviews_ID_Product_fkey");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("Services_pkey");

            entity.Property(e => e.IdService).HasColumnName("ID_Service");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
