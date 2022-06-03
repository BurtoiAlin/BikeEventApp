using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BikeEvent.Entities
{
    public partial class LicentaDatabaseContext : DbContext
    {
        public LicentaDatabaseContext()
        {
        }

        public LicentaDatabaseContext(DbContextOptions<LicentaDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryAge> CategoryAges { get; set; }
        public virtual DbSet<CategoryType> CategoryTypes { get; set; }
        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClub> UserClubs { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=LicentaDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("category_ID");

                entity.Property(e => e.CategoryAgeId).HasColumnName("categoryAge_ID");

                entity.Property(e => e.CategoryTypeId).HasColumnName("categoryType_ID");

                entity.Property(e => e.GenderId).HasColumnName("gender_ID");

                entity.HasOne(d => d.CategoryAge)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.CategoryAgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Categories_CategoryAge");

                entity.HasOne(d => d.CategoryType)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.CategoryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Categories_CategoryType");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Categories_Gender");
            });

            modelBuilder.Entity<CategoryAge>(entity =>
            {
                entity.ToTable("CategoryAge");

                entity.Property(e => e.CategoryAgeId)
                    .ValueGeneratedNever()
                    .HasColumnName("categoryAge_ID");

                entity.Property(e => e.EndAge).HasColumnName("end_age");

                entity.Property(e => e.StartingAge).HasColumnName("starting_age");
            });

            modelBuilder.Entity<CategoryType>(entity =>
            {
                entity.ToTable("CategoryType");

                entity.Property(e => e.CategoryTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("categoryType_ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.Property(e => e.ClubId)
                    .ValueGeneratedNever()
                    .HasColumnName("club_ID");

                entity.Property(e => e.ClubDescription).HasColumnName("club_description");

                entity.Property(e => e.ClubName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("club_name");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId)
                    .ValueGeneratedNever()
                    .HasColumnName("event_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.EventDescription)
                    .IsRequired()
                    .HasColumnName("event_description");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("event_name");

                entity.Property(e => e.EventPic)
                    .IsRequired()
                    .HasColumnName("event_pic");

                entity.Property(e => e.MapPath)
                    .IsRequired()
                    .HasColumnName("map_path");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("gender_ID");

                entity.Property(e => e.GenderName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("gender_name");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.Property(e => e.ResultId)
                    .ValueGeneratedNever()
                    .HasColumnName("result_ID");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.EventId).HasColumnName("event_ID");

                entity.Property(e => e.StartingTime)
                    .HasColumnType("datetime")
                    .HasColumnName("starting_time");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Results_Events");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Results_Users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_ID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_ID");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("birth_date");

                entity.Property(e => e.ClubId).HasColumnName("club_ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.GenderId).HasColumnName("gender_ID");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.RoleId).HasColumnName("role_ID");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Gender");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            modelBuilder.Entity<UserClub>(entity =>
            {
                entity.Property(e => e.UserClubId)
                    .ValueGeneratedNever()
                    .HasColumnName("userClub_ID");

                entity.Property(e => e.ClubId).HasColumnName("club_ID");

                entity.Property(e => e.ExitDate)
                    .HasColumnType("datetime")
                    .HasColumnName("exit_date");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registration_date");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.UserClubs)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserClubs_Clubs");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClubs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserClubs_Users");
            });

            modelBuilder.Entity<UserEvent>(entity =>
            {
                entity.HasKey(e => e.UserEventsId);

                entity.Property(e => e.UserEventsId)
                    .ValueGeneratedNever()
                    .HasColumnName("userEvents_ID");

                entity.Property(e => e.EventId).HasColumnName("event_ID");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.UserEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserEvents_Events");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEvents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserEvents_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
