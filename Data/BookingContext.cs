using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTicketingApplication.Models;

namespace MovieTicketingApplication.Data
{
    public partial class BookingContext : DbContext
    {
        private readonly IConfiguration _config;

        public BookingContext(DbContextOptions<BookingContext> options, IConfiguration configuration)
            : base(options)
        {
            _config = configuration;
        }

        public virtual DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_config.GetConnectionString("DbConnection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {

                entity.ToTable("bookings");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BookingDate).HasColumnName("booking_date");
                entity.Property(e => e.BookingTime).HasColumnName("booking_time");
                entity.Property(e => e.Movieid).HasColumnName("movieid");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.SeatsString)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("seatsString");
                entity.Property(e => e.Theatreid).HasColumnName("theatreid");
                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Movie).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Movieid)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Theatre).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Theatreid)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
