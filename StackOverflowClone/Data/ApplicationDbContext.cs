using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackOverflowClone.Models;

namespace StackOverflowClone.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public virtual DbSet<Badge> Badges { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<LinkType> LinkTypes { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<PostLink> PostLinks { get; set; }

        public virtual DbSet<PostType> PostTypes { get; set; }

        public virtual DbSet<PostUser> PostUsers { get; set; }

        public virtual DbSet<QuestionsAnswer> QuestionsAnswers { get; set; }

        public virtual DbSet<Vote> Votes { get; set; }

        public virtual DbSet<VoteType> VoteTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Name=DefaultConnectionString");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Badge>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Badges__Id");

                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.Name).HasMaxLength(40);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).HasMaxLength(256);
                entity.Property(e => e.Auth).HasDefaultValue("");
                entity.Property(e => e.Endpoint).HasDefaultValue("");
                entity.Property(e => e.P256dh)
                    .HasDefaultValue("")
                    .HasColumnName("P256DH");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Comments__Id");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
                entity.Property(e => e.Text).HasMaxLength(700);
            });

            modelBuilder.Entity<LinkType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_LinkTypes__Id");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Posts__Id");

                entity.HasIndex(e => e.AcceptedAnswerId, "IX_Posts__AcceptedAnswerId");

                entity.HasIndex(e => e.CreationDate, "IX_Posts__CreationDate").IsDescending();

                entity.HasIndex(e => e.ParentId, "IX_Posts__ParentId");

                entity.HasIndex(e => e.PostTypeId, "IX_Posts__PostTypeId");

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");
                entity.Property(e => e.CommunityOwnedDate).HasColumnType("datetime");
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");
                entity.Property(e => e.LastEditDate).HasColumnType("datetime");
                entity.Property(e => e.LastEditorDisplayName).HasMaxLength(40);
                entity.Property(e => e.Tags).HasMaxLength(150);
                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<PostLink>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_PostLinks__Id");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_PostTypes__Id");

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<PostUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Users_Id");

                entity.HasIndex(e => e.CreationDate, "IX_Users__CreationDate").IsDescending();

                entity.HasIndex(e => e.LastAccessDate, "IX_Users__LastAccessDate").IsDescending();

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
                entity.Property(e => e.DisplayName).HasMaxLength(40);
                entity.Property(e => e.EmailHash).HasMaxLength(40);
                entity.Property(e => e.LastAccessDate).HasColumnType("datetime");
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.WebsiteUrl).HasMaxLength(200);
            });

            modelBuilder.Entity<QuestionsAnswer>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("QuestionsAnswers");

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");
                entity.Property(e => e.CommunityOwnedDate).HasColumnType("datetime");
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");
                entity.Property(e => e.LastEditDate).HasColumnType("datetime");
                entity.Property(e => e.LastEditorDisplayName).HasMaxLength(40);
                entity.Property(e => e.Tags).HasMaxLength(150);
                entity.Property(e => e.Title).HasMaxLength(250);
                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Votes__Id");

                entity.HasIndex(e => e.CreationDate, "IX_Votes__CreationDate").IsDescending();

                entity.HasIndex(e => e.PostId, "IX_Votes__PostId");

                entity.HasIndex(e => e.VoteTypeId, "IX_Votes__VoteTypeId");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VoteType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_VoteType__Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

        }

    }
}
