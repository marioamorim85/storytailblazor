using Microsoft.EntityFrameworkCore;
using StoryTailBlazor.Models;

namespace StoryTailBlazor.Data
{
    public class StoryTailDbContext : DbContext
    {
        public StoryTailDbContext(DbContextOptions<StoryTailDbContext> options) : base(options)
        {
        }

        // DbSets - Tabelas da Base de Dados
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityBook> ActivityBooks { get; set; }
        public DbSet<ActivityBookUser> ActivityBookUsers { get; set; }
        public DbSet<ActivityImage> ActivityImages { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<BookUserRead> BookUserReads { get; set; }
        public DbSet<BookUserFavourite> BookUserFavourites { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentModeration> CommentModerations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaggingTagged> TaggingTaggeds { get; set; }
        public DbSet<BookClick> BookClicks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<AgeGroup> AgeGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de chaves compostas e relacionamentos muitos-para-muitos
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorId, ab.BookId });
            

            modelBuilder.Entity<BookUserRead>()
                .HasKey(br => new { br.BookId, br.UserId });

            modelBuilder.Entity<BookUserFavourite>()
                .HasKey(bf => new { bf.BookId, bf.UserId });

            modelBuilder.Entity<TaggingTagged>()
                .HasKey(tt => new { tt.BookId, tt.TagId });
            
            modelBuilder.Entity<ActivityBookUser>()
                .HasKey(abu => new { abu.ActivityBookId, abu.UserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}


