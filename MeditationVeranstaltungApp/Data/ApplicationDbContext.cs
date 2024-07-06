using MeditationVeranstaltungApp.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeditationVeranstaltungApp.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
                }, new Kontakt
                 {
                     Id = 2,
                     Vorname = "Jaspal",
                     Nachname = "Singh",
                     Anrede = Anrede.Herr,
                     Geschlecht = Geschlecht.Männlich,
                     Email = "jaspal.singht@meditationcamp.com",
                     Telefon = "+49 754 5678 5789",
                     Stadt = "Hamburg",
                     Land = "DE"
                 }
                );

           
            modelBuilder.Entity<GastInfo>().HasData(
               new GastInfo
               {
                   Id = 1,
                   AnzahlMaenner =2,
                   AnzahlWeiblich =1,
                   AnkunftAm = DateTime.Parse("2024-08-14T16:20:55.630Z"),
                   AnkunftOrt = "Flughagen",
                   AbfahrtAm = DateTime.Parse("2024-08-14T16:20:55.630Z"),
                   AbfahrtOrt = "Flughagen",
                   KontaktId=1,
                   FahrerKontaktId=2,
                   Veranstalltung ="SAS2024HH",
                   UserId= "93df0a45-7232-45e6-b882-8dcc0966ba8a"
               }
               );


            modelBuilder.Entity<IdentityRole>().HasData(
                 new IdentityRole
                 {
                     Name = "User",
                     NormalizedName = "USER",
                     Id = "a609455d-5f11-42bd-be9d-5fd66e4570c0"
                 },
                 new IdentityRole
                 {
                     Name = "Driver",
                     NormalizedName = "DRIVER",
                     Id = "b2b71169-bcf7-41c6-90c5-3f808fd7cafa"
                 },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = "28aaaaad-012d-4951-b501-5813b2a541ff"
                }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "ec673be3-bdfe-4ee6-9ff1-3712a38acb5e",
                    Email = "admin@web.de",
                    NormalizedEmail = "ADMIN@WEB.DE",
                    UserName = "admin@web.de",
                    NormalizedUserName = "ADMIN@WEB.DE",
                    PasswordHash = hasher.HashPassword(null, "Waheguru@1"),
                    FirstName="Admin",
                    LastName=""
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "28aaaaad-012d-4951-b501-5813b2a541ff",
                    UserId = "ec673be3-bdfe-4ee6-9ff1-3712a38acb5e"
                }
            );


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
