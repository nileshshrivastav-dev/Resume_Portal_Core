using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Resume_Portal.Models;

public partial class ResumePortalArivaniContext : DbContext
{
    public ResumePortalArivaniContext()
    {
    }

    public ResumePortalArivaniContext(DbContextOptions<ResumePortalArivaniContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblDesignation> TblDesignations { get; set; }

    public virtual DbSet<TblResume> TblResumes { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-V640GNA;Database=Resume_Portal_Arivani;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblDesignation>(entity =>
        {
            entity.HasKey(e => e.Designation).HasName("PK__tbl_Desi__5638C9D64DD27A24");

            entity.ToTable("tbl_Designation");

            entity.Property(e => e.Designation)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblResume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbl_resu__3213E83F7B32FD5B");

            entity.ToTable("tbl_resume");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Designation)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.FilePath)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.KeyWords)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Resume)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.UploadedBy)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity.HasOne(d => d.DesignationNavigation).WithMany(p => p.TblResumes)
                .HasForeignKey(d => d.Designation)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__tbl_resum__Desig__4CA06362");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.TblResumes)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__tbl_resum__Email__4BAC3F29");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__tbl_User__A9D1053561BCC2A8");

            entity.ToTable("tbl_User");

            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DateOfJoining).HasColumnName("Date_of_Joining");
            entity.Property(e => e.Designation)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Photo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
