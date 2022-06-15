using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HMS_BE.Models
{
    public partial class HMSContext : DbContext
    {
        public HMSContext()
        {
        }

        public HMSContext(DbContextOptions<HMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AllowedWorkGroup> AllowedWorkGroups { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<HelpRequest> HelpRequests { get; set; }
        public virtual DbSet<Leader> Leaders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Work> Works { get; set; }
        public virtual DbSet<WorkTicket> WorkTickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AllowedWorkGroup>(entity =>
            {
                entity.ToTable("AllowedWorkGroup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AllowedWorkGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__AllowedWo__Group__35BCFE0A");

                entity.HasOne(d => d.Work)
                    .WithMany(p => p.AllowedWorkGroups)
                    .HasForeignKey(d => d.WorkId)
                    .HasConstraintName("FK__AllowedWo__WorkI__34C8D9D1");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.ToTable("GroupUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsLeader).HasColumnName("isLeader");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__GroupUser__Group__2D27B809");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__GroupUser__UserI__2C3393D0");
            });

            modelBuilder.Entity<HelpRequest>(entity =>
            {
                entity.ToTable("HelpRequest");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CreationUserId).HasColumnName("CreationUserID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CreationUser)
                    .WithMany(p => p.HelpRequests)
                    .HasForeignKey(d => d.CreationUserId)
                    .HasConstraintName("FK__HelpReque__Creat__6383C8BA");
            });

            modelBuilder.Entity<Leader>(entity =>
            {
                entity.ToTable("Leader");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsLeader).HasColumnName("isLeader");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Leaders)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Leader__GroupId__60A75C0F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Leaders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Leader__UserId__5EBF139D");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bio).IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(140);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__UserRole__RoleId__286302EC");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserRole__UserId__276EDEB3");
            });

            modelBuilder.Entity<Work>(entity =>
            {
                entity.ToTable("Work");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CreationUserId).HasColumnName("CreationUserID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CreationUser)
                    .WithMany(p => p.Works)
                    .HasForeignKey(d => d.CreationUserId)
                    .HasConstraintName("FK__Work__CreationUs__31EC6D26");
            });

            modelBuilder.Entity<WorkTicket>(entity =>
            {
                entity.HasKey(e => new { e.OwnerId, e.WorkId, e.GroupId })
                    .HasName("Constraint_UserWorkGroup");

                entity.ToTable("WorkTicket");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.WorkTickets)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkTicke__Group__6B24EA82");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.WorkTickets)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkTicke__Owner__6A30C649");

                entity.HasOne(d => d.Work)
                    .WithMany(p => p.WorkTickets)
                    .HasForeignKey(d => d.WorkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkTicke__WorkI__693CA210");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
