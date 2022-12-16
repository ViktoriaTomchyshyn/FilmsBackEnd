using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FilmsWebApi.Models;

public partial class FilmsDbContext : DbContext
{
    public FilmsDbContext()
    {
    }

    public FilmsDbContext(DbContextOptions<FilmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Recommendation> Recommendations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-JJTRH2D;Initial Catalog=FilmsDB;Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__COMMENTS__3213E83FED076390");

            entity.ToTable("COMMENTS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment1)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FILMS__3213E83FBB70EFAA");

            entity.ToTable("FILMS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Link)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("link");
        });

        modelBuilder.Entity<Recommendation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RECOMMEN__3213E83F65F5F014");

            entity.ToTable("RECOMMENDATIONS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.RecomendedFilmId).HasColumnName("recomended_film_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
