using FlowerShopBusinessObject.Common;
using FlowerShopBusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopBusinessObject.DBContext
{
    public partial class ApplicationDBContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<FlowerBouquet> FlowerBouquet { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            modelBuilder.Entity<OrderDetail>()
                .HasKey(r => new { r.OrderID, r.FlowerBouquetID });

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = Guid.Parse("D38A7009-FA0C-4FD1-815F-49879DFCBFB7"),
                    AccountPassword = "@5",
                    FullName = "Administrator",
                    EmailAddress = "admin@gmail.com",
                    Role = 1
                },
                new Account
                {
                    Id = Guid.Parse("C71F13E6-3A8F-4BFA-975E-C05DAC1707EB"),
                    AccountPassword = "@5",
                    FullName = "Staff",
                    EmailAddress = "staff@gmail.com",
                    Role = 2
                },
                new Account
                {
                    Id = Guid.Parse("4694A7A2-E609-4BFC-BD6F-6F082367181D"),
                    AccountPassword = "@5",
                    FullName = "Manager",
                    EmailAddress = "manager@gmail.com",
                    Role = 3
                },
                new Account
                {
                    Id = Guid.Parse("2477CB57-B562-469D-8F78-0A96663CB5E2"),
                    AccountPassword = "@5",
                    FullName = "Customer",
                    EmailAddress = "customer@gmail.com",
                    Role = 4
                }
                );
            /*modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(od => new { od.OrderID, od.FlowerBouquetID });

                entity.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(od => od.FlowerBouquet)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.FlowerBouquetID)
                .OnDelete(DeleteBehavior.NoAction);
            });*/

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.Parse("2C81C8F6-CAE1-46A6-9BC4-71F29F6DA74E"), CategoryName = "Rose", Description = "This is Rose" },
                new Category { Id = Guid.Parse("B7101212-C852-4BE6-8EED-031B096B2DD4"), CategoryName = "Peonies", Description = "This is Peonies" },
                new Category { Id = Guid.Parse("CB85B07B-2FCC-497C-9007-8912A86C2F4F"), CategoryName = "Lily", Description = "This is Lily" },
                new Category { Id = Guid.Parse("D302148F-1677-4094-8FA7-1C6B54FF8B69"), CategoryName = "Carnation", Description = "This is Carnation" }
                );
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                    Id = Guid.Parse("FC1D6720-4461-418C-8680-8AD859EDA033"),
                    SupplierName = "Flower Shop Ciaoflora",
                    SupplierAddress = "Dai Hoc FPT, Ha Noi",
                    Telephone = "0123456789"
                },
                new Supplier
                {
                    Id = Guid.Parse("F60DB79F-1C7E-4B2B-A3F2-4551942CCDD6"),
                    SupplierName = "Ant Flower",
                    SupplierAddress = "Dai Hoc FPT Co So 2, Ha Noi",
                    Telephone = "0123456789"
                }
                );
            modelBuilder.Entity<FlowerBouquet>().HasData(
                new FlowerBouquet()
                {
                    Id = Guid.Parse("6CB4DD24-4DEC-4EC2-8DAB-5D677F11CFFB"),
                    FlowerBouquetName = "Red Rose",
                    Description = "This is Rose",
                    UnitPrice = 100000,
                    UnitsInStock = 100,
                    FlowerBouquetStatus = 1,
                    CategoryID = Guid.Parse("2C81C8F6-CAE1-46A6-9BC4-71F29F6DA74E"),
                    SupplierID = Guid.Parse("FC1D6720-4461-418C-8680-8AD859EDA033")
                },
                new FlowerBouquet()
                {
                    Id = Guid.Parse("CCF6F1D5-BEDA-4832-8EAE-1D82280693C0"),
                    FlowerBouquetName = "Orchis",
                    Description = "This is Orchis",
                    UnitPrice = 100000,
                    UnitsInStock = 100,
                    FlowerBouquetStatus = 1,
                    CategoryID = Guid.Parse("2C81C8F6-CAE1-46A6-9BC4-71F29F6DA74E"),
                    SupplierID = Guid.Parse("FC1D6720-4461-418C-8680-8AD859EDA033")
                },
                new FlowerBouquet()
                {
                    Id = Guid.Parse("4CEE7A0F-17F9-4F49-AA3C-8DCE72B8ABE1"),
                    FlowerBouquetName = "Sun Flower",
                    Description = "This is Sun Flower",
                    UnitPrice = 100000,
                    UnitsInStock = 100,
                    FlowerBouquetStatus = 1,
                    CategoryID = Guid.Parse("2C81C8F6-CAE1-46A6-9BC4-71F29F6DA74E"),
                    SupplierID = Guid.Parse("FC1D6720-4461-418C-8680-8AD859EDA033")
                }
                );
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseAuditableEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified)) {
                entry.Entity.LastModified = DateTime.Now;
                if (entry.State == EntityState.Added) {
                    entry.Entity.Created = DateTime.Now;
                }
                if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted) {
                    entry.Entity.LastModified = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
    }
}
