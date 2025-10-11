using Microsoft.EntityFrameworkCore;
using HospitalApplicationAPI.Models;
using HospitalApplicationAPI.Models.Request; // Adjust based on your project structure

namespace HospitalApplicationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Patient> Patient { get; set; }


        public DbSet<Doctor> Doctor { get; set; }

        
        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<Member> Members { get; set; }

        // Add other tables like Doctors, Staff, etc. later

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ This tells EF: "It's not a table — just map query results"
            modelBuilder.Entity<CheckPatientRequest>().HasNoKey();
            modelBuilder.Entity<GetDoctorsRequest>().HasNoKey();
        }
    }
}
