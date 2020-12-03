using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PROG3050_CVGSClub.Models
{
    public partial class CvgsClubContext : DbContext
    {
        public CvgsClubContext()
        {
        }

        public CvgsClubContext(DbContextOptions<CvgsClubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<FriendsFamily> FriendsFamily { get; set; }
        public virtual DbSet<GameReviews> GameReviews { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<GamesLibrary> GamesLibrary { get; set; }
        public virtual DbSet<MemberEvents> MemberEvents { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<WishLists> WishLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress19;Database=cvgs_club;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("events");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.EventDate)
                    .HasColumnName("event_date")
                    .HasColumnType("date");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("event_name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnName("start_time");
            });

            modelBuilder.Entity<FriendsFamily>(entity =>
            {
                entity.HasKey(e => e.FriendFamilyId);

                entity.ToTable("friends_family");

                entity.Property(e => e.FriendFamilyId).HasColumnName("friend_family_id");

                entity.Property(e => e.FriendId)
                    .IsRequired()
                    .HasColumnName("friend_id")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId)
                    .IsRequired()
                    .HasColumnName("member_id")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.FriendsFamily)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("friends_family_fk_members");
            });

            modelBuilder.Entity<GameReviews>(entity =>
            {
                entity.HasKey(e => e.ReviewId);

                entity.ToTable("gameReviews");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.GameReview)
                    .IsRequired()
                    .HasColumnName("game_review")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId)
                    .IsRequired()
                    .HasColumnName("member_id")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameReviews)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gameReviews_fk_games");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.GameReviews)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gameReviews_fk_members");
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.ToTable("games");

                entity.HasIndex(e => e.GameName)
                    .HasName("UQ__games__CDFC05C495303A8E")
                    .IsUnique();

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.AvailablePlatforms)
                    .IsRequired()
                    .HasColumnName("available_platforms")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContentRating)
                    .IsRequired()
                    .HasColumnName("content_rating")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('RP')");

                entity.Property(e => e.GameName)
                    .IsRequired()
                    .HasColumnName("game_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasColumnName("genre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ListPrice)
                    .HasColumnName("list_price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MaxPlayers)
                    .IsRequired()
                    .HasColumnName("max_players")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GamesLibrary>(entity =>
            {
                entity.HasKey(e => e.LibraryGameId);

                entity.ToTable("gamesLibrary");

                entity.Property(e => e.LibraryGameId).HasColumnName("libraryGame_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.MemberId)
                    .IsRequired()
                    .HasColumnName("member_id")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GamesLibrary)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gamesLibrary_fk_games");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.GamesLibrary)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gamesLibrary_fk_members");
            });

            modelBuilder.Entity<MemberEvents>(entity =>
            {
                entity.ToTable("memberEvents");

                entity.Property(e => e.MemberEventsId).HasColumnName("member_events_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.MemberId)
                    .IsRequired()
                    .HasColumnName("member_id")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.MemberEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("events_fk_events");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberEvents)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("events_fk_members");
            });

            modelBuilder.Entity<Members>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("members");

                entity.Property(e => e.MemberId)
                    .HasColumnName("member_id")
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.CardExpires)
                    .HasColumnName("card_expires")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.CardNumber)
                    .HasColumnName("card_number")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CardType)
                    .HasColumnName("card_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasColumnName("display_name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveEmails)
                    .HasColumnName("receive_emails")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<WishLists>(entity =>
            {
                entity.HasKey(e => e.WishId);

                entity.ToTable("wish_lists");

                entity.Property(e => e.WishId).HasColumnName("wish_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.MemberId)
                    .IsRequired()
                    .HasColumnName("member_id")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wish_lists_fk_games");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wish_lists_fk_members");
            });
        }
    }
}
