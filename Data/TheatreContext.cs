using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTicketingApplication.Models;

namespace MovieTicketingApplication.Data
{
    public partial class TheatreContext : DbContext
    {
        private readonly IConfiguration _config;

        public TheatreContext(DbContextOptions<TheatreContext> options, IConfiguration configuration)
            : base(options)
        {
            _config = configuration;
        }

        public virtual DbSet<Theatre> Theatres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_config.GetConnectionString("DbConnection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Theatre>(entity =>
            {

                entity.ToTable("theatres");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BookedSeats)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("bookedSeats");
                entity.Property(e => e.ConvFee)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("convFee");
                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("location");
                entity.Property(e => e.ShowTimings)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("showTimings");
                entity.Property(e => e.TheatreName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("theatreName");
                entity.Property(e => e.TicketFare)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("ticketFare");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
