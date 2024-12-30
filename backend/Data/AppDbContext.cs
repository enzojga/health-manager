using backend.Models;
using Microsoft.EntityFrameworkCore;
namespace backend.Data 
{
    public class MyDbContext : DbContext
    {
        public required DbSet<Worker> Workers { get; set; }
        public required DbSet<Room> Rooms { get; set; }
        public required DbSet<Patient> Patients { get; set; }
        public required DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Banco.health-manager");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Doctor)
                .WithOne(w => w.PatientAsDoctor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Nurse)
                .WithOne(w => w.PatientAsNurse)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}