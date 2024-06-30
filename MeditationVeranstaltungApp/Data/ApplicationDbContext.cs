﻿using MeditationVeranstaltungApp.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeditationVeranstaltungApp.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Kontakt> Kontakts { get; set; } = null!;
        public virtual DbSet<GastInfo> GastInfos { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kontakt>(entity =>
            {
                entity.Property(e => e.Vorname).HasMaxLength(50);

                entity.Property(e => e.Nachname).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Telefon).HasMaxLength(50);
                entity.Property(e => e.Stadt).HasMaxLength(50);
                entity.Property(e => e.Land).HasMaxLength(50);
            });

            modelBuilder.Entity<GastInfo>(entity =>
            {
                entity.Property(e => e.Veranstalltung).HasMaxLength(100);

                entity.Property(e => e.AnkunftOrt).HasMaxLength(50);
                entity.Property(e => e.AbfahrtOrt).HasMaxLength(100);
                entity.Property(e => e.Notiz).HasMaxLength(250);
            });

            modelBuilder.Entity<GastInfo>()
            .HasOne(e => e.Kontakt)
            .WithMany(e => e.GastInfos)
            .HasForeignKey(e => e.KontaktId)
            .IsRequired();

            modelBuilder.Entity<Kontakt>().HasData(
                new Kontakt
                {
                    Id = 1,
                    Vorname = "Parminder",
                    Nachname = "Singh",
                    Anrede = Anrede.Herr,
                    Geschlecht = Geschlecht.Männlich,
                    Email = "parminder.singht@meditationcamp.com",
                    Telefon = "+44 123 5678 9911",
                    Stadt = "Slough",
                    Land = "UK"
                }
                );

            modelBuilder.Entity<GastInfo>().HasData(
               new GastInfo
               {
                   Id = 1,
                   AnkunftAm = DateTime.Parse("2024-08-14T16:20:55.630Z"),
                   AnkunftOrt = "Flughagen",
                   AbfahrtAm = DateTime.Parse("2024-08-14T16:20:55.630Z"),
                   AbfahrtOrt = "Flughagen",
                   KontaktId=1,
                   Veranstalltung ="SAS2024HH",
                   UserId= "93df0a45-7232-45e6-b882-8dcc0966ba8a"
               }
               );



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
