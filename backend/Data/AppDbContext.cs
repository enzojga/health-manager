using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public required DbSet<Worker> Workers { get; set; }
    public required DbSet<Room> Rooms { get; set; }
    public required DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Banco.helth-manager");
        base.OnConfiguring(optionsBuilder);
    }
}
