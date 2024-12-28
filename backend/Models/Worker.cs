public class Worker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public WorkerType Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}