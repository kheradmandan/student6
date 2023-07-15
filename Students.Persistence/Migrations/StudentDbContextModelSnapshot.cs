﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Students.Persistence.DbContexts;

#nullable disable

namespace Students.Persistence.Migrations
{
    [DbContext(typeof(StudentDbContext))]
    partial class StudentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Students.Domain.Entities.CI_FieldCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CI_FieldCourses");
                });

            modelBuilder.Entity("Students.Domain.Entities.CI_FieldStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CI_FieldStudents");
                });

            modelBuilder.Entity("Students.Domain.Entities.CI_Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CI_Roles");
                });

            modelBuilder.Entity("Students.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CI_FieldCourseId")
                        .HasColumnType("int");

                    b.Property<int>("Classtime")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("TimeOfSession")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CI_FieldCourseId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Students.Domain.Entities.CourseStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Nomre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseStudents");
                });

            modelBuilder.Entity("Students.Domain.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("CI_FieldStudentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("NationalCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CI_FieldStudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Students.Domain.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Mob")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Students.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CI_RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CI_RoleId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Students.Domain.Entities.Course", b =>
                {
                    b.HasOne("Students.Domain.Entities.CI_FieldCourse", "CI_FieldCourse")
                        .WithMany("Courses")
                        .HasForeignKey("CI_FieldCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CI_FieldCourse");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Students.Domain.Entities.CourseStudent", b =>
                {
                    b.HasOne("Students.Domain.Entities.Course", "courses")
                        .WithMany("CourseStudent")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Domain.Entities.Student", "Students")
                        .WithMany("CourseStudent")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Students");

                    b.Navigation("courses");
                });

            modelBuilder.Entity("Students.Domain.Entities.Student", b =>
                {
                    b.HasOne("Students.Domain.Entities.CI_FieldStudent", "CI_FieldStudent")
                        .WithMany("Students")
                        .HasForeignKey("CI_FieldStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CI_FieldStudent");
                });

            modelBuilder.Entity("Students.Domain.Entities.User", b =>
                {
                    b.HasOne("Students.Domain.Entities.CI_Role", "cI_Role")
                        .WithMany("Users")
                        .HasForeignKey("CI_RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Domain.Entities.Student", null)
                        .WithMany("Users")
                        .HasForeignKey("StudentId");

                    b.HasOne("Students.Domain.Entities.Teacher", null)
                        .WithMany("Users")
                        .HasForeignKey("TeacherId");

                    b.Navigation("cI_Role");
                });

            modelBuilder.Entity("Students.Domain.Entities.CI_FieldCourse", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Students.Domain.Entities.CI_FieldStudent", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Students.Domain.Entities.CI_Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Students.Domain.Entities.Course", b =>
                {
                    b.Navigation("CourseStudent");
                });

            modelBuilder.Entity("Students.Domain.Entities.Student", b =>
                {
                    b.Navigation("CourseStudent");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Students.Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
