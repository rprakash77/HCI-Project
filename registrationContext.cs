using System;
using System.Collections.Generic;
using HCI_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;

namespace HCI_Project
{
    public partial class registrationContext : DbContext
    {
        public registrationContext()
        {
        }

        public registrationContext(DbContextOptions<registrationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hastaken> Hastakens { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Takeableclass> Takeableclasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string[] lines = File.ReadAllLines(@"C:\Users\Public\Documents\registration.txt");
                string connString = string.Format("Host={0};Username={1};Database=registration", lines[0], lines[1]);
                var builder = new NpgsqlConnectionStringBuilder(connString)
                {
                    Password = lines[2]
                };
                optionsBuilder.UseNpgsql(builder.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hastaken>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("hastaken");

                entity.Property(e => e.Crn)
                    .HasPrecision(5)
                    .HasColumnName("crn");

                entity.Property(e => e.Uid)
                    .HasPrecision(6)
                    .HasColumnName("uid");

                entity.HasOne(d => d.CrnNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Crn)
                    .HasConstraintName("hastaken_crn_fkey");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("hastaken_uid_fkey");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.Crn)
                    .HasName("sections_pkey");

                entity.ToTable("sections");

                entity.Property(e => e.Crn)
                    .HasPrecision(5)
                    .HasColumnName("crn");

                entity.Property(e => e.Classid).HasColumnName("classid");

                entity.Property(e => e.Days)
                    .HasMaxLength(20)
                    .HasColumnName("days");

                entity.Property(e => e.Locations)
                    .HasMaxLength(100)
                    .HasColumnName("locations");

                entity.Property(e => e.Prof)
                    .HasMaxLength(100)
                    .HasColumnName("prof");

                entity.Property(e => e.Timeslot)
                    .HasMaxLength(20)
                    .HasColumnName("timeslot");

                entity.Property(e => e.Typeofclass)
                    .HasMaxLength(20)
                    .HasColumnName("typeofclass");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.Classid)
                    .HasConstraintName("sections_classid_fkey");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("student_pkey");

                entity.ToTable("student");

                entity.Property(e => e.Uid)
                    .HasPrecision(6)
                    .HasColumnName("uid");

                entity.Property(e => e.Fname)
                    .HasMaxLength(30)
                    .HasColumnName("fname");

                entity.Property(e => e.Holds)
                    .HasMaxLength(256)
                    .HasColumnName("holds");

                entity.Property(e => e.Lname)
                    .HasMaxLength(30)
                    .HasColumnName("lname");

                entity.Property(e => e.Startdate).HasColumnName("startdate");
            });

            modelBuilder.Entity<Takeableclass>(entity =>
            {
                entity.HasKey(e => e.Classid)
                    .HasName("takeableclass_pkey");

                entity.ToTable("takeableclass");

                entity.Property(e => e.Classid)
                    .ValueGeneratedNever()
                    .HasColumnName("classid");

                entity.Property(e => e.Classname)
                    .HasMaxLength(100)
                    .HasColumnName("classname");

                entity.Property(e => e.Code)
                    .HasPrecision(4)
                    .HasColumnName("code");

                entity.Property(e => e.Prefix)
                    .HasMaxLength(3)
                    .HasColumnName("prefix");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
