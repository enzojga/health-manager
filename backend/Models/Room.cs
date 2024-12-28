public class Room
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<Patient>? Patients { get; set; }
}