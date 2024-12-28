using System.ComponentModel.DataAnnotations;

public class WorkerDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EnumDataType(typeof(WorkerType), ErrorMessage = "Type must be either 'Doctor' or 'Nurse'")]
    public WorkerType Type { get; set; }
}