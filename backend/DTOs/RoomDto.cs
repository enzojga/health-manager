using System.ComponentModel.DataAnnotations;

public class RoomDto
{
    [Required]
    public int Capacity { get; set; }
}