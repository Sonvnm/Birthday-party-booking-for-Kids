using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models
{
    public partial class BirthdayPartyBookingForKids_DBContext : DbContext
    {
        public BirthdayPartyBookingForKids_DBContext()
        {
        }

        public BirthdayPartyBookingForKids_DBContext(DbContextOptions<BirthdayPartyBookingForKids_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Decoration> Decorations { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.BookingId)
                    .HasMaxLength(100)
                    .HasColumnName("BookingID");

                entity.Property(e => e.DateBooking).HasColumnType("date");

                entity.Property(e => e.KidBirthDay).HasColumnType("date");

                entity.Property(e => e.KidName).HasMaxLength(100);

                entity.Property(e => e.LocationId)
                    .HasMaxLength(100)
                    .HasColumnName("LocationID");

                entity.Property(e => e.ServiceId)
                    .HasMaxLength(100)
                    .HasColumnName("ServiceID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Booking__Locatio__5EBF139D");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Booking__Service__5FB337D6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Booking__UserID__5DCAEF64");
            });

            modelBuilder.Entity<Decoration>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__Decorati__727E83EB059E55A3");

                entity.ToTable("Decoration");

                entity.Property(e => e.ItemId)
                    .HasMaxLength(100)
                    .HasColumnName("ItemID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.ItemName).HasMaxLength(100);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.FoodId)
                    .HasName("PK__Menu__856DB3CBB536D2E0");

                entity.ToTable("Menu");

                entity.Property(e => e.FoodId)
                    .HasMaxLength(100)
                    .HasColumnName("FoodID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.FoodName).HasMaxLength(100);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId)
                    .HasMaxLength(100)
                    .HasColumnName("PaymentID");

                entity.Property(e => e.BankId)
                    .HasMaxLength(100)
                    .HasColumnName("BankID");

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BookingId)
                    .HasMaxLength(100)
                    .HasColumnName("BookingID");

                entity.Property(e => e.MoneyReceiver).HasMaxLength(100);

                entity.Property(e => e.PaymentTypeId)
                    .HasMaxLength(100)
                    .HasColumnName("PaymentTypeID");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__Payment__Booking__6383C8BA");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK__Payment__Payment__628FA481");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentType");

                entity.Property(e => e.PaymentTypeId)
                    .HasMaxLength(100)
                    .HasColumnName("PaymentTypeID");

                entity.Property(e => e.PaymentTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(20)
                    .HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__Room__E7FEA47725DBDD7F");

                entity.ToTable("Room");

                entity.Property(e => e.LocationId)
                    .HasMaxLength(100)
                    .HasColumnName("LocationID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.LocationName).HasMaxLength(100);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId)
                    .HasMaxLength(100)
                    .HasColumnName("ServiceID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.FoodId)
                    .HasMaxLength(100)
                    .HasColumnName("FoodID");

                entity.Property(e => e.ItemId)
                    .HasMaxLength(100)
                    .HasColumnName("ItemID");

                entity.Property(e => e.ServiceName).HasMaxLength(100);

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__Service__FoodID__5441852A");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__Service__ItemID__5535A963");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .HasColumnName("UserID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(11);

                entity.Property(e => e.RoleId)
                    .HasMaxLength(20)
                    .HasColumnName("RoleID");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User__RoleID__4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
