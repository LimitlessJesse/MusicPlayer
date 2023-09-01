using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Models.DataModels;

namespace MusicPlayer.Models.Database
{
    public class MusicPlayerDbContext : IdentityDbContext<User>
    {
        public MusicPlayerDbContext(DbContextOptions<MusicPlayerDbContext> options) : base(options) 
        {
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Playlists)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Songs)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();

            modelBuilder.Entity<Song>()
                .HasKey(e => new { e.SourceId, e.UserId });

            modelBuilder.Entity<Song>()
                .HasOne(e => e.User)
                .WithMany(e => e.Songs)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
