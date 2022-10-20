using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using API.Entities;

namespace API.Context
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext() { }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<address> addresses { get; set; } = null!;
        public virtual DbSet<booked_extra> booked_extras { get; set; } = null!;
        public virtual DbSet<booked_room> booked_rooms { get; set; } = null!;
        public virtual DbSet<booking> bookings { get; set; } = null!;
        public virtual DbSet<booking_message> booking_messages { get; set; } = null!;
        public virtual DbSet<extra> extras { get; set; } = null!;
        public virtual DbSet<extra_type> extra_types { get; set; } = null!;
        public virtual DbSet<feedback> feedbacks { get; set; } = null!;
        public virtual DbSet<payment> payments { get; set; } = null!;
        public virtual DbSet<promotion> promotions { get; set; } = null!;
        public virtual DbSet<role> roles { get; set; } = null!;
        public virtual DbSet<room> rooms { get; set; } = null!;
        public virtual DbSet<room_type> room_types { get; set; } = null!;
        public virtual DbSet<user> users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=146.230.177.46;Initial Catalog=GroupPmb7;Persist Security Info=True;User ID=GroupPmb7;Password=8yrrvz");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<address>(entity =>
            {
                entity.HasOne(d => d.customer)
                    .WithOne(p => p.address)
                    .HasForeignKey<address>(d => d.customer_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("address_customer_id_fk");
            });

            modelBuilder.Entity<booked_extra>(entity =>
            {
                entity.HasOne(d => d.booking)
                    .WithOne(p => p.booked_extra)
                    .HasForeignKey<booked_extra>(d => d.booking_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("booking_extra_id");

                entity.HasOne(d => d.extra)
                    .WithMany(p => p.booked_extras)
                    .HasForeignKey(d => d.extra_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("extra_id");
            });

            modelBuilder.Entity<booked_room>(entity =>
            {
                entity.HasKey(e => e.booking_id)
                    .HasName("booked_room_pk");

                entity.Property(e => e.booking_id).ValueGeneratedNever();

                entity.HasOne(d => d.booking)
                    .WithOne(p => p.booked_room)
                    .HasForeignKey<booked_room>(d => d.booking_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booked_room_book__fk");

                entity.HasOne(d => d.room)
                    .WithMany(p => p.booked_rooms)
                    .HasForeignKey(d => d.room_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booked_room_room_fk");
            });

            modelBuilder.Entity<booking>(entity =>
            {
                entity.Property(e => e.date).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.cardNavigation)
                    .WithMany(p => p.bookings)
                    .HasForeignKey(d => d.card)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_payment_id_fk");

                entity.HasOne(d => d.customerNavigation)
                    .WithMany(p => p.bookings)
                    .HasForeignKey(d => d.customer)
                    .HasConstraintName("customer_booking_id");

                entity.HasOne(d => d.promotionNavigation)
                    .WithMany(p => p.bookings)
                    .HasForeignKey(d => d.promotion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_promotion_id_fk");
            });

            modelBuilder.Entity<booking_message>(entity =>
            {
                entity.HasOne(d => d.bookingNavigation)
                    .WithOne(p => p.booking_message)
                    .HasForeignKey<booking_message>(d => d.booking)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("booking_message_booking_id_fk");
            });

            modelBuilder.Entity<feedback>(entity =>
            {
                entity.HasOne(d => d.bookingNavigation)
                    .WithOne(p => p.feedback)
                    .HasForeignKey<feedback>(d => d.booking)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("feedback_booking_fk");
            });

            modelBuilder.Entity<payment>(entity =>
            {
                entity.HasOne(d => d.customer)
                    .WithMany(p => p.payments)
                    .HasForeignKey(d => d.customer_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("customer_payment_id");
            });

            modelBuilder.Entity<room>(entity =>
            {
                entity.HasOne(d => d.type)
                    .WithMany(p => p.rooms)
                    .HasForeignKey(d => d.type_id)
                    .HasConstraintName("room_room_type_id_fk");
            });

            modelBuilder.Entity<room_type>(entity =>
            {
                entity.HasOne(d => d.imageNavigation)
                    .WithMany(p => p.room_types)
                    .HasForeignKey(d => d.image)
                    .HasConstraintName("room_type_image_fk");
            });

            modelBuilder.Entity<user>(entity =>
            {
                entity.HasOne(d => d.imageNavigation)
                    .WithMany(p => p.users)
                    .HasForeignKey(d => d.image)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_image_fk");

                entity.HasOne(d => d.roleNavigation)
                    .WithMany(p => p.users)
                    .HasForeignKey(d => d.role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_role_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
