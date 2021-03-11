﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using V.NetCore.Repository;

namespace V.NetCore.Repository.Migrations
{
    [DbContext(typeof(VDbContext))]
    [Migration("20190112082911_updateRole")]
    partial class updateRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("V.NetCore.Repository.Domain.Button", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Code")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Explain")
                        .HasColumnName("Explain")
                        .HasMaxLength(500);

                    b.Property<string>("Icon")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("Sort");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("Buttons");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.Department", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("CascadeId")
                        .HasColumnName("CascadeId")
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("CreateTime");

                    b.Property<string>("Explain")
                        .HasColumnName("Explain")
                        .HasMaxLength(500);

                    b.Property<bool>("IsAble")
                        .HasColumnName("IsAble");

                    b.Property<string>("Label");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("ParentId")
                        .HasColumnName("ParentId")
                        .HasMaxLength(50);

                    b.Property<string>("ParentName")
                        .HasColumnName("ParentName")
                        .HasMaxLength(50);

                    b.Property<int>("Sort")
                        .HasColumnName("Sort");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnName("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.Menu", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("CascadeId")
                        .HasColumnName("CascadeId")
                        .HasMaxLength(100);

                    b.Property<string>("Code")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Icon")
                        .HasMaxLength(50);

                    b.Property<string>("Label");

                    b.Property<string>("LinkAddress")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("ParentId")
                        .HasColumnName("ParentId")
                        .HasMaxLength(50);

                    b.Property<string>("ParentName")
                        .HasColumnName("ParentName")
                        .HasMaxLength(50);

                    b.Property<int>("Sort");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.MenuButton", b =>
                {
                    b.Property<string>("MenuId");

                    b.Property<string>("ButtonId");

                    b.HasKey("MenuId", "ButtonId");

                    b.HasIndex("ButtonId");

                    b.ToTable("MenuButton");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.OutBox", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<int>("ComPort");

                    b.Property<bool>("IsDel");

                    b.Property<string>("Mbno")
                        .HasColumnName("Mbno");

                    b.Property<string>("Msg")
                        .HasMaxLength(200);

                    b.Property<int>("Report");

                    b.Property<DateTime>("SendTime");

                    b.Property<string>("username")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("OutBox");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Explain")
                        .HasMaxLength(500);

                    b.Property<bool>("IsAble");

                    b.Property<bool>("IsDel");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("Sort");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.RoleMenu", b =>
                {
                    b.Property<string>("MenuId");

                    b.Property<string>("RoleId");

                    b.HasKey("MenuId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleMenus");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDel");

                    b.Property<bool>("IsFinger");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("RowNum");

                    b.Property<int>("Sex");

                    b.Property<int>("Status");

                    b.Property<string>("Tel");

                    b.Property<DateTime>("UpdateTime");

                    b.Property<string>("UserNumber");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.UserDepartment", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("DepartmentId");

                    b.HasKey("UserId", "DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("UserDepartment");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.MenuButton", b =>
                {
                    b.HasOne("V.NetCore.Repository.Domain.Button", "Button")
                        .WithMany("MenuButtons")
                        .HasForeignKey("ButtonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("V.NetCore.Repository.Domain.Menu", "Menu")
                        .WithMany("MenuButtons")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.RoleMenu", b =>
                {
                    b.HasOne("V.NetCore.Repository.Domain.Menu", "Menu")
                        .WithMany("RoeMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("V.NetCore.Repository.Domain.Role", "Role")
                        .WithMany("RoleMenus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.UserDepartment", b =>
                {
                    b.HasOne("V.NetCore.Repository.Domain.Department", "Department")
                        .WithMany("UserDepartments")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("V.NetCore.Repository.Domain.User", "User")
                        .WithMany("UserDepartments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("V.NetCore.Repository.Domain.UserRole", b =>
                {
                    b.HasOne("V.NetCore.Repository.Domain.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("V.NetCore.Repository.Domain.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}