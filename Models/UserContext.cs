using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MovieTicketingApplication.Models
{
    public partial class UserContext : DbContext
    {
        private readonly IConfiguration _config;

        public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration)
            : base(options)
        {
            _config = configuration;
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_config.GetConnectionString("DbConnection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {

                entity.ToTable("users");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Dob).HasColumnName("DOB");
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.FirstName).HasMaxLength(255);
                entity.Property(e => e.Gender).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(255);
                entity.Property(e => e.PasswordHashed).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
