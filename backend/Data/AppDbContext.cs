using Microsoft.EntityFrameworkCore;

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

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasOne(p => p.Doctor)
                .WithOne()
                .HasForeignKey<Patient>(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Nurse)
                .WithOne()
                .HasForeignKey<Patient>(p => p.NurseId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
