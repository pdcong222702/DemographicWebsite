using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Demographic_Website.ModelViews;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Demographic_Website.Models;

public partial class DemoGraphicContext : IdentityDbContext<ApplicationUser>
{
    public DemoGraphicContext()
    {
    }

    public DemoGraphicContext(DbContextOptions<DemoGraphicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Population> Populations { get; set; }

    public virtual DbSet<ResidenceBooklet> ResidenceBooklets { get; set; }

    public virtual DbSet<TemporarilyAbsent> TemporarilyAbsents { get; set; }

    public virtual DbSet<TemporarilyStaying> TemporarilyStayings { get; set; }
    public virtual DbSet<SpiltResidentce> SpiltResidentces { get; set; }
    public virtual DbSet<MoveResidence> MoveResidences { get; set; }
    public virtual DbSet<Province> Provinces { get; set; }
    public virtual DbSet<District> Districts { get; set; }
    public virtual DbSet<Wards> Wards { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-JPDGHD0\\SQLEXPRESS;Database=DemoGraphic;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var item in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = item.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                item.SetTableName(tableName.Substring(6));
            }
        }

        modelBuilder.Entity<Population>(entity =>
        {
            entity.HasKey(e => e.PopulationId).HasName("PK__Populati__3A2E15E20414EC74");

            entity.Property(e => e.BirthPlace).HasMaxLength(250);
            entity.Property(e => e.CitizenIdentificationCard).HasMaxLength(12);
            entity.Property(e => e.Ethnicity).HasMaxLength(100);
            entity.Property(e => e.Occupation).HasMaxLength(100);
            entity.Property(e => e.PlaceOfIssue).HasMaxLength(250);
            entity.Property(e => e.PopulationName).HasMaxLength(100);
            entity.Property(e => e.PopulationNickName).HasMaxLength(100);
            entity.Property(e => e.Relationship)
                .HasMaxLength(50)
                .IsUnicode(true);
            entity.Property(e => e.Religion).HasMaxLength(100);
            entity.Property(e => e.WorkPlace).HasMaxLength(250);

            entity.HasOne(d => d.ResidenceBooklet).WithMany(p => p.Populations)
                .HasForeignKey(d => d.ResidenceBookletId)
                .HasConstraintName("FK_ResidenceBooklet");
        });

        modelBuilder.Entity<ResidenceBooklet>(entity =>
        {
            entity.HasKey(e => e.ResidenceBookletId).HasName("PK__Residenc__AE46D5A2295AD5E1");

            entity.ToTable("ResidenceBooklet");

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.BookletArea).HasMaxLength(100);
            entity.Property(e => e.ResidenceBookletCode).HasMaxLength(100);
        });

        modelBuilder.Entity<TemporarilyAbsent>(entity =>
        {
            entity.HasKey(e => e.TemporarilyAbsentId).HasName("PK__Temporar__FB890209EFD0C9ED");

            entity.ToTable("TemporarilyAbsent");

            entity.Property(e => e.BirthPlace).HasMaxLength(250);
            entity.Property(e => e.CitizenIdentificationCard).HasMaxLength(12);
            entity.Property(e => e.Nationality).HasMaxLength(250);
            entity.Property(e => e.IsNewAbsent).HasMaxLength(100);
            entity.Property(e => e.PopulationName).HasMaxLength(100);

            entity.HasOne(d => d.Population).WithMany(p => p.TemporarilyAbsents)
                .HasForeignKey(d => d.PopulationId)
                .HasConstraintName("FK_PopulationAbsent");
        });

        modelBuilder.Entity<TemporarilyStaying>(entity =>
        {
            entity.HasKey(e => e.TemporarilyStayingId).HasName("PK__Temporar__FB89020962FB23B0");

            entity.ToTable("TemporarilyStaying");

            entity.Property(e => e.BirthPlace).HasMaxLength(250);
            entity.Property(e => e.CitizenIdentificationCard).HasMaxLength(12);
            entity.Property(e => e.Nationality).HasMaxLength(250);
            entity.Property(e => e.IsNewStay).HasMaxLength(100);
            entity.Property(e => e.PopulationName).HasMaxLength(100);
            entity.Property(e => e.TemporaryAddress).HasMaxLength(255);

            entity.HasOne(d => d.Population).WithMany(p => p.TemporarilyStayings)
                .HasForeignKey(d => d.PopulationId)
                .HasConstraintName("FK_PopulationStaying");
        });

        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<Demographic_Website.ModelViews.BookLet> BookLet { get; set; } = default!;
}
