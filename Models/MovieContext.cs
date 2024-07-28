using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MovieTicketingApplication.Models
{
    public partial class MovieContext : DbContext
    {
        private readonly IConfiguration _config;

        public MovieContext(DbContextOptions<MovieContext> options, IConfiguration configuration)
            : base(options)
        {
            _config = configuration;
        }

        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_config.GetConnectionString("DbConnection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {

                entity.ToTable("movies");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");
                entity.Property(e => e.Duration).HasColumnName("duration");
                entity.Property(e => e.Lang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lang");
                entity.Property(e => e.Poster)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("poster");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
                entity.Property(e => e.Trailer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("trailer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
