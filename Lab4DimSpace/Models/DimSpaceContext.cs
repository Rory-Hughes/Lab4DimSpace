using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab4DimSpace.Models;

public partial class DimSpaceContext : DbContext
{
    public DimSpaceContext()
    {
    }

    public DimSpaceContext(DbContextOptions<DimSpaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseAccess> CourseAccesses { get; set; }

    public virtual DbSet<DropBox> DropBoxes { get; set; }

    public virtual DbSet<DropBoxItem> DropBoxItems { get; set; }

    public virtual DbSet<DropBoxStatus> DropBoxStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DimSpace;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A7A765D010");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CourseAccess>(entity =>
        {
            entity.HasKey(e => e.CourseAccessId).HasName("PK__CourseAc__14EFAA094051DB4B");

            entity.ToTable("CourseAccess");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseAccesses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CourseAcc__Cours__5441852A");

            entity.HasOne(d => d.User).WithMany(p => p.CourseAccesses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CourseAcc__UserI__534D60F1");
        });

        modelBuilder.Entity<DropBox>(entity =>
        {
            entity.HasKey(e => e.DropBoxId).HasName("PK__DropBox__27F13A3DA9002E1A");

            entity.ToTable("DropBox");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.DropBoxes)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__DropBox__CourseI__571DF1D5");
        });

        modelBuilder.Entity<DropBoxItem>(entity =>
        {
            entity.HasKey(e => e.DropBoxItemId).HasName("PK__DropBoxI__07D28D5EC934327E");

            entity.HasOne(d => d.DropBox).WithMany(p => p.DropBoxItems)
                .HasForeignKey(d => d.DropBoxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DropBoxIt__DropB__59FA5E80");

            entity.HasOne(d => d.Status).WithMany(p => p.DropBoxItems)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DropBoxIt__Statu__5BE2A6F2");

            entity.HasOne(d => d.Student).WithMany(p => p.DropBoxItems)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DropBoxIt__Stude__5AEE82B9");
        });

        modelBuilder.Entity<DropBoxStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__DropBoxS__C8EE20632060F328");

            entity.ToTable("DropBoxStatus");

            entity.Property(e => e.StatusName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CBC70222D");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__UserRoleI__5070F446");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A3519BF4A36");

            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
