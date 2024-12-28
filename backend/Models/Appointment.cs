using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    [Key]
    public int Id { get; set; }

    public bool Finished { get; set; } = false;

    [ForeignKey("Patient")]
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public Patient Patient { get; set; }
}