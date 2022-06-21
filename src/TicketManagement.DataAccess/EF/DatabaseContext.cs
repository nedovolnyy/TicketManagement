using System;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.EF
{
    public partial class DatabaseContext : DbContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string connectionString)
            : base()
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventArea> EventAreas { get; set; } = null!;
        public virtual DbSet<EventSeat> EventSeats { get; set; } = null!;
        public virtual DbSet<Layout> Layouts { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Venue> Venues { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    builder => builder.EnableRetryOnFailure()).LogTo(Console.WriteLine);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.LayoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Layout_Area");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.EventEndTime).HasPrecision(0);

                entity.Property(e => e.EventTime).HasPrecision(0);

                entity.Property(e => e.Name).HasMaxLength(120);

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.LayoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Layout_Event");
            });

            modelBuilder.Entity<EventArea>(entity =>
            {
                entity.ToTable("EventArea");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventAreas)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_EventArea");
            });

            modelBuilder.Entity<EventSeat>(entity =>
            {
                entity.ToTable("EventSeat");

                entity.HasOne(d => d.EventArea)
                    .WithMany(p => p.EventSeats)
                    .HasForeignKey(d => d.EventAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventArea_EventSeat");
            });

            modelBuilder.Entity<Layout>(entity =>
            {
                entity.ToTable("Layout");

                entity.Property(e => e.Description).HasMaxLength(120);

                entity.Property(e => e.Name).HasMaxLength(120);

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.Layouts)
                    .HasForeignKey(d => d.VenueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Venue_Layout");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("Seat");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_Seat");
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.ToTable("Venue");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(120);

                entity.Property(e => e.Name).HasMaxLength(120);

                entity.Property(e => e.Phone).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
