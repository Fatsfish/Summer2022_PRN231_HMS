﻿// <auto-generated />
using System;
using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HMS_BE.Migrations
{
    [DbContext(typeof(HMSContext))]
    [Migration("20220627043013_LazyTony")]
    partial class LazyTony
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HMS_BE.Models.AllowedWorkGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("WorkId");

                    b.ToTable("AllowedWorkGroup");
                });

            modelBuilder.Entity("HMS_BE.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("isDelete");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("HMS_BE.Models.GroupUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLeader")
                        .HasColumnType("bit")
                        .HasColumnName("isApproved");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUser");
                });

            modelBuilder.Entity("HMS_BE.Models.HelpRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("CreationUserId")
                        .HasColumnType("int")
                        .HasColumnName("CreationUserID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("isDelete");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CreationUserId");

                    b.ToTable("HelpRequest");
                });

            modelBuilder.Entity("HMS_BE.Models.Leader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLeader")
                        .HasColumnType("bit")
                        .HasColumnName("isLeader");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Votes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Leader");
                });

            modelBuilder.Entity("HMS_BE.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("isDelete");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("HMS_BE.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("isActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HMS_BE.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("HMS_BE.Models.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("CreationUserId")
                        .HasColumnType("int")
                        .HasColumnName("CreationUserID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("isDelete");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<double>("Progress")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CreationUserId");

                    b.ToTable("Work");
                });

            modelBuilder.Entity("HMS_BE.Models.WorkTicket", b =>
                {
                    b.Property<int>("OwnerId")
                        .HasColumnType("int")
                        .HasColumnName("OwnerID");

                    b.Property<int>("WorkId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("isDelete");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("OwnerId", "WorkId", "GroupId")
                        .HasName("Constraint_UserWorkGroup");

                    b.HasIndex("GroupId");

                    b.HasIndex("WorkId");

                    b.ToTable("WorkTicket");
                });

            modelBuilder.Entity("HMS_BE.Models.AllowedWorkGroup", b =>
                {
                    b.HasOne("HMS_BE.Models.Group", "Group")
                        .WithMany("AllowedWorkGroups")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK__AllowedWo__Group__35BCFE0A");

                    b.HasOne("HMS_BE.Models.Work", "Work")
                        .WithMany("AllowedWorkGroups")
                        .HasForeignKey("WorkId")
                        .HasConstraintName("FK__AllowedWo__WorkI__34C8D9D1");

                    b.Navigation("Group");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("HMS_BE.Models.GroupUser", b =>
                {
                    b.HasOne("HMS_BE.Models.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK__GroupUser__Group__2D27B809");

                    b.HasOne("HMS_BE.Models.User", "User")
                        .WithMany("GroupUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__GroupUser__UserI__2C3393D0");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HMS_BE.Models.HelpRequest", b =>
                {
                    b.HasOne("HMS_BE.Models.User", "CreationUser")
                        .WithMany("HelpRequests")
                        .HasForeignKey("CreationUserId")
                        .HasConstraintName("FK__HelpReque__Creat__6383C8BA");

                    b.Navigation("CreationUser");
                });

            modelBuilder.Entity("HMS_BE.Models.Leader", b =>
                {
                    b.HasOne("HMS_BE.Models.Group", "Group")
                        .WithMany("Leaders")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK__Leader__GroupId__60A75C0F");

                    b.HasOne("HMS_BE.Models.User", "User")
                        .WithMany("Leaders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Leader__UserId__5EBF139D");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HMS_BE.Models.UserRole", b =>
                {
                    b.HasOne("HMS_BE.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__UserRole__RoleId__286302EC");

                    b.HasOne("HMS_BE.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserRole__UserId__276EDEB3");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HMS_BE.Models.Work", b =>
                {
                    b.HasOne("HMS_BE.Models.User", "CreationUser")
                        .WithMany("Works")
                        .HasForeignKey("CreationUserId")
                        .HasConstraintName("FK__Work__CreationUs__31EC6D26");

                    b.Navigation("CreationUser");
                });

            modelBuilder.Entity("HMS_BE.Models.WorkTicket", b =>
                {
                    b.HasOne("HMS_BE.Models.Group", "Group")
                        .WithMany("WorkTickets")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK__WorkTicke__Group__6B24EA82")
                        .IsRequired();

                    b.HasOne("HMS_BE.Models.User", "Owner")
                        .WithMany("WorkTickets")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK__WorkTicke__Owner__6A30C649")
                        .IsRequired();

                    b.HasOne("HMS_BE.Models.Work", "Work")
                        .WithMany("WorkTickets")
                        .HasForeignKey("WorkId")
                        .HasConstraintName("FK__WorkTicke__WorkI__693CA210")
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Owner");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("HMS_BE.Models.Group", b =>
                {
                    b.Navigation("AllowedWorkGroups");

                    b.Navigation("GroupUsers");

                    b.Navigation("Leaders");

                    b.Navigation("WorkTickets");
                });

            modelBuilder.Entity("HMS_BE.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("HMS_BE.Models.User", b =>
                {
                    b.Navigation("GroupUsers");

                    b.Navigation("HelpRequests");

                    b.Navigation("Leaders");

                    b.Navigation("UserRoles");

                    b.Navigation("Works");

                    b.Navigation("WorkTickets");
                });

            modelBuilder.Entity("HMS_BE.Models.Work", b =>
                {
                    b.Navigation("AllowedWorkGroups");

                    b.Navigation("WorkTickets");
                });
#pragma warning restore 612, 618
        }
    }
}
