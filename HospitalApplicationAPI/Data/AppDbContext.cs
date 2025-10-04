using Microsoft.EntityFrameworkCore;
using HospitalApplicationAPI.Models; // Adjust based on your project structure

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
    }
}
