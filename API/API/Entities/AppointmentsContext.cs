using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Entities
{
    public partial class AppointmentsContext : DbContext
    {
        public AppointmentsContext()
        {
        }

        public AppointmentsContext(DbContextOptions<AppointmentsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dayslots> Dayslots { get; set; }
        public virtual DbSet<Meetings> Meetings { get; set; }
        public virtual DbSet<Meetingstatus> Meetingstatus { get; set; }
        public virtual DbSet<Timeslots> Timeslots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=Appointments;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Dayslots>(entity =>
            {
                entity.HasKey(e => e.IdDay)
                    .HasName("PK__DAYSLOTS__0E65962A612636D4");

                entity.ToTable("DAYSLOTS");

                entity.Property(e => e.Day).HasColumnType("date");
            });

            modelBuilder.Entity<Meetings>(entity =>
            {
                entity.HasKey(e => e.IdMeeting)
                    .HasName("PK__MEETINGS__392A178A3678BAF2");

                entity.ToTable("MEETINGS");

                entity.HasOne(d => d.IdDayNavigation)
                    .WithMany(p => p.Meetings)
                    .HasForeignKey(d => d.IdDay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Day");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Meetings)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MEETINGSTATUS");

                entity.HasOne(d => d.IdTimeslotNavigation)
                    .WithMany(p => p.Meetings)
                    .HasForeignKey(d => d.IdTimeslot)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TimeSlots");
            });

            modelBuilder.Entity<Meetingstatus>(entity =>
            {
                entity.HasKey(e => e.IdMeSt)
                    .HasName("PK__MEETINGS__4D7D8D6C1ACBC87C");

                entity.ToTable("MEETINGSTATUS");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Timeslots>(entity =>
            {
                entity.HasKey(e => e.IdTimeslot)
                    .HasName("PK__TIMESLOT__7713A400FED9695A");

                entity.ToTable("TIMESLOTS");

                entity.Property(e => e.EndTime).HasColumnName("End_time");

                entity.Property(e => e.StartTime).HasColumnName("Start_time");
            });
        }
    }
}
