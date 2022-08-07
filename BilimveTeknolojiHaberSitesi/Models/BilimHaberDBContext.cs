using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BilimveTeknolojiHaberSitesi.Models
{
    public partial class BilimHaberDBContext : DbContext
    {
        public BilimHaberDBContext()
        {
        }

        public BilimHaberDBContext(DbContextOptions<BilimHaberDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }
        public virtual DbSet<Makaleler> Makalelers { get; set; }
        public virtual DbSet<Menuler> Menulers { get; set; }
        public virtual DbSet<Sayfalar> Sayfalars { get; set; }
        public virtual DbSet<YetkiTb> YetkiTbs { get; set; }
        public virtual DbSet<Yorumlar> Yorumlars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=BilimHaberDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Kullanicilar>(entity =>
            {
                entity.HasKey(e => e.KullaniciId)
                    .HasName("PK__Kullanic__E011F09B82AE3689");

                entity.ToTable("Kullanicilar");

                entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");

                entity.Property(e => e.Adı).HasMaxLength(150);

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.EklenmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Eposta).HasMaxLength(100);

                entity.Property(e => e.Parola).HasMaxLength(50);

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Soyadı).HasMaxLength(150);

                entity.HasOne(d => d.Yetki)
                    .WithMany(p => p.Kullanicilars)
                    .HasForeignKey(d => d.YetkiId)
                    .HasConstraintName("FK_Kullanicilar_YetkiTB");
            });

            modelBuilder.Entity<Makaleler>(entity =>
            {
                entity.HasKey(e => e.MakaleId)
                    .HasName("PK__Makalele__DA97CBAD6AD368F7");

                entity.ToTable("Makaleler");

                entity.Property(e => e.MakaleId).HasColumnName("MakaleID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Baslik)
                    .HasMaxLength(200)
                    .HasColumnName("baslik");

                entity.Property(e => e.Eklenmetarihi)
                    .HasColumnType("datetime")
                    .HasColumnName("eklenmetarihi");

                entity.Property(e => e.Icerik)
                    .HasColumnType("ntext")
                    .HasColumnName("icerik");

                entity.Property(e => e.KulId).HasColumnName("KulID");

                entity.Property(e => e.Resim)
                    .HasMaxLength(150)
                    .HasColumnName("resim");

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Sira).HasColumnName("sira");
            });

            modelBuilder.Entity<Menuler>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK__Menuler__C99ED25000B9473C");

                entity.ToTable("Menuler");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Baslik)
                    .HasMaxLength(200)
                    .HasColumnName("baslik");

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Sira).HasColumnName("sira");

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .HasColumnName("url");

                entity.Property(e => e.UstId).HasColumnName("UstID");
            });

            modelBuilder.Entity<Sayfalar>(entity =>
            {
                entity.HasKey(e => e.SayfaId)
                    .HasName("PK__Sayfalar__F81FC2B2D7DCECDA");

                entity.ToTable("Sayfalar");

                entity.Property(e => e.SayfaId).HasColumnName("sayfaID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Baslik)
                    .HasMaxLength(200)
                    .HasColumnName("baslik");

                entity.Property(e => e.EklenmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Icerik)
                    .HasColumnType("ntext")
                    .HasColumnName("icerik");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.Resim)
                    .HasMaxLength(150)
                    .HasColumnName("resim");

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Sayfalars)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_Sayfalar_Menuler");
            });

            modelBuilder.Entity<YetkiTb>(entity =>
            {
                entity.HasKey(e => e.YetkiId);

                entity.ToTable("YetkiTB");

                entity.Property(e => e.YetkiId).HasColumnName("YetkiID");

                entity.Property(e => e.YetkiAdi).HasMaxLength(100);
            });

            modelBuilder.Entity<Yorumlar>(entity =>
            {
                entity.HasKey(e => e.YorumId)
                    .HasName("PK__Yorumlar__222191BF738A1EC1");

                entity.ToTable("Yorumlar");

                entity.Property(e => e.YorumId).HasColumnName("yorumID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.MakaleId).HasColumnName("MakaleID");

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Tarih)
                    .HasColumnType("datetime")
                    .HasColumnName("tarih");

                entity.Property(e => e.Yorum).HasMaxLength(550);

                entity.HasOne(d => d.Kul)
                    .WithMany(p => p.Yorumlars)
                    .HasForeignKey(d => d.KulId)
                    .HasConstraintName("FK__Yorumlar__KulId__412EB0B6");

                entity.HasOne(d => d.Makale)
                    .WithMany(p => p.Yorumlars)
                    .HasForeignKey(d => d.MakaleId)
                    .HasConstraintName("FK_Yorumlar_Makaleler");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
