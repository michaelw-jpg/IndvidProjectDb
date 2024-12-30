using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab3;

public partial class labb2Context : DbContext
{
    public labb2Context()
    {
    }

    public labb2Context(DbContextOptions<labb2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ActiveEnrollment> ActiveEnrollments { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=labb2;Integrated Security=True;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActiveEnrollment>(entity =>
        {
            entity.HasKey(e => e.ActiveCourseId).HasName("PK_ActiveCourses");

            entity.Property(e => e.ActiveCourseId).HasColumnName("ActiveCourseID");
            entity.Property(e => e.FkCourseId).HasColumnName("fkCourseID");
            entity.Property(e => e.FkStudentId).HasColumnName("FkStudentID");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.ActiveEnrollments)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK_ActiveCourses_Courses");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A0336FC187");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassName).HasMaxLength(50);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D7187374E0B72");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.FkTeacherId).HasColumnName("fkTeacherID");
            entity.Property(e => e.Subjects).HasMaxLength(50);

            entity.HasOne(d => d.FkTeacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FkTeacherId)
                .HasConstraintName("FK__Courses__fkTeach__3E52440B");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF16805395C");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Department).HasMaxLength(32);
            entity.Property(e => e.FirstName).HasMaxLength(32);
            entity.Property(e => e.LastName).HasMaxLength(32);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__54F87A37E192F7BE");

            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.FkCourseId).HasColumnName("fkCourseID");
            entity.Property(e => e.FkStudentId).HasColumnName("fkStudentID");
            entity.Property(e => e.FkTeacherId).HasColumnName("fkTeacherID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(2)
                .HasColumnName("Grade");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK__Grades__fkCourse__403A8C7D");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK__Grades__fkStuden__6477ECF3");

            entity.HasOne(d => d.FkTeacher).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkTeacherId)
                .HasConstraintName("FK__Grades__fkTeache__3F466844");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A79966D87FB");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.FirstName).HasMaxLength(32);
            entity.Property(e => e.FkClassId).HasColumnName("fkClassID");
            entity.Property(e => e.LastName).HasMaxLength(32);
            entity.Property(e => e.Personnummer).HasMaxLength(13);

            entity.HasOne(d => d.FkClass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkClassId)
                .HasConstraintName("FK__Students__fkClas__6754599E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
