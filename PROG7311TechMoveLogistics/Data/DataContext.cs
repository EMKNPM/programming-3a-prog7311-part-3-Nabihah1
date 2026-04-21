using PROG7311TechMoveLogistics.Models;
using Microsoft.EntityFrameworkCore; 


namespace PROG7311TechMoveLogistics.Data
{ 

        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<Client> Clients { get; set; }
            public DbSet<Contract> Contracts { get; set; }
            public DbSet<ServiceRequest> ServiceRequests { get; set; }
            public DbSet<UploadedDocument> UploadedDocuments { get; set; }



            //adding fluent api on model creating for extra constraints ??
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Client>()
                    .Property(c => c.ClientName)
                    .IsRequired()
                    .HasMaxLength(100);

                modelBuilder.Entity<Client>()
                    .Property(c => c.ContactDetails)
                    .IsRequired()
                    .HasMaxLength(150);

                modelBuilder.Entity<Client>()
                    .Property(c => c.Region)
                    .IsRequired()
                    .HasMaxLength(100);

                modelBuilder.Entity<Contract>()
                    .HasOne(c => c.Client)
                    .WithMany(c => c.Contracts)
                    .HasForeignKey(c => c.ClientId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<ServiceRequest>()
                    .HasOne(s => s.Contract)
                    .WithMany(c => c.ServiceRequests)
                    .HasForeignKey(s => s.ContractId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<UploadedDocument>()
                    .HasOne(d => d.Contract)
                    .WithMany(c => c.Documents)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.Cascade);
            }




        }
    }
