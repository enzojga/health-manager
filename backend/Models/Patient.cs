using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Patient
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Cpf { get; set; }
    [ForeignKey("Room")]
    public int? RoomId { get; set; }

    [ForeignKey("Worker")]
    public int? DoctorId { get; set; }
    [ForeignKey("Worker")]
    public int? NurseId { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    [JsonIgnore]
    public Room? Room { get; set; }
    public Worker? Doctor { get; set; }
    public Worker? Nurse { get; set; }
}