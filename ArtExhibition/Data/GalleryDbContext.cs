using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ArtExhibition.Models;

namespace ArtExhibition.Data
{
    public class GalleryDbContext : IdentityDbContext<User>
    {
        public GalleryDbContext(DbContextOptions<GalleryDbContext> options)
            : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<RoomRequest> RoomRequests { get; set; }
        public DbSet<ArtistRequest> ArtistRequests { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<ExhibitionArtist> ExhibitionArtists { get; set; }
        public DbSet<ExhibitionArtwork> ExhibitionArtworks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User-Cart relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Explicitly set to NoAction

            // Configure CartItem-Cart relationship
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure CartItem-Artwork relationship
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Artwork)
                .WithMany()
                .HasForeignKey(ci => ci.ArtworkId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Artist-Artwork relationship
            modelBuilder.Entity<Artwork>()
                .HasOne(a => a.Artist)
                .WithMany(artist => artist.Artworks)
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Room-Artwork relationship
            modelBuilder.Entity<Artwork>()
                .HasOne(a => a.Room)
                .WithMany(r => r.Artworks)
                .HasForeignKey(a => a.RoomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ExhibitionArtist>()
                .HasKey(ea => new { ea.ExhibitionId, ea.ArtistId });

            modelBuilder.Entity<ExhibitionArtist>()
                .HasOne(ea => ea.Exhibition)
                .WithMany(e => e.ExhibitionArtists)
                .HasForeignKey(ea => ea.ExhibitionId);

            modelBuilder.Entity<ExhibitionArtist>()
                .HasOne(ea => ea.Artist)
                .WithMany(a => a.ExhibitionArtists)
                .HasForeignKey(ea => ea.ArtistId);


            // Configure many-to-many for Exhibition and Artwork
            modelBuilder.Entity<ExhibitionArtwork>()
                .HasKey(ea => new { ea.ExhibitionId, ea.ArtworkId });

            modelBuilder.Entity<ExhibitionArtwork>()
                .HasOne(ea => ea.Exhibition)
                .WithMany(e => e.ExhibitionArtworks)
                .HasForeignKey(ea => ea.ExhibitionId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<ExhibitionArtwork>()
                .HasOne(ea => ea.Artwork)
                .WithMany(a => a.ExhibitionArtworks)
                .HasForeignKey(ea => ea.ArtworkId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
