using ST10062630_PROG6212_POE.Models;
using Microsoft.EntityFrameworkCore;


namespace ST10062630_PROG6212_POE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // Users table
        public DbSet<UserRole> Roles { get; set; } // Roles table
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the foreign key relationship between User and UserRole
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role) // User has one Role
                .WithMany()           // Role does not have a navigation property for Users
                .HasForeignKey(u => u.UserRoleId) // Corrected to match the renamed property
                .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascading deletes

            // Define the primary key for UserRole explicitly
            modelBuilder.Entity<UserRole>()
                .HasKey(r => r.UserRoleId); // Corrected to use the new property name
        }

    }

}
