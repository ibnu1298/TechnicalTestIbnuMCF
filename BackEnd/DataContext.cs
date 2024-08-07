using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BackEnd
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<TrBpkb> TrBpkbs { get; set; }
        public DbSet<MsUser> MsUsers { get; set; }
        public DbSet<MsStorageLocation> MsStorageLocations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MCFDB")
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
            Microsoft.Extensions.Logging.LogLevel.Information).EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TrBpkb>(entity =>
            {
                entity.HasKey(e => e.AgreementNumber).HasName("PK_tr_bpkb");

                entity.ToTable("tr_bpkb");

                entity.HasOne(d => d.MsStorageLocation).WithMany(p => p.TrBpkbs)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_tr_bpkb_ms_storage_location");

                entity.Property(e => e.AgreementNumber).HasColumnName("agreement_number").HasMaxLength(100);
                entity.Property(e => e.BpkbNo).HasColumnName("bpkb_no").HasMaxLength(100);
                entity.Property(e => e.BranchId).HasColumnName("branch_id").HasMaxLength(10);
                entity.Property(e => e.FakturNo).HasColumnName("faktur_no").HasMaxLength(100);
                entity.Property(e => e.LocationId).HasColumnName("location_id").HasMaxLength(10);
                entity.Property(e => e.PoliceNo).HasColumnName("police_no").HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasColumnName("created_by").HasMaxLength(20);
                entity.Property(e => e.LastUpdatedBy).HasColumnName("last_updated_by").HasMaxLength(20);

                entity.Property(e => e.BpkbDateIn)
                .HasColumnName("bpkb_date_in")
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

                entity.Property(e => e.FakturDate)
                .HasColumnName("faktur_date")
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

                entity.Property(e => e.BpkbDate)
                .HasColumnName("bpkb_date")
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

                entity.Property(e => e.CreatedOn)
                .HasColumnName("created_on")
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedOn)
                .HasColumnName("last_updated_on")
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            });

            modelBuilder.Entity<MsStorageLocation>(entity => {
                entity.HasKey(e => e.LocationId).HasName("PK_ms_storage_location");

                entity.ToTable("ms_storage_location");

                entity.Property(e => e.LocationId).HasColumnName("location_id").HasMaxLength(10);
                entity.Property(e => e.LocationName).HasColumnName("location_name").HasMaxLength(100);
            });

            modelBuilder.Entity<MsUser>(entity => {
                entity.HasKey(e => e.UserId).HasName("PK_ms_user");

                entity.ToTable("ms_user");

                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.UserName).HasColumnName("user_name").HasMaxLength(20);
                entity.Property(e => e.Password).HasColumnName("password").HasMaxLength(50);
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            });
        }

    }
}
