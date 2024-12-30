using System.ComponentModel.DataAnnotations;
namespace backend.DTOS
{
    public class PatientDto
    {
        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Cpf { get; set; } = "";

        public int? RoomId { get; set; }
        public int? DoctorId { get; set; }
        public int? NurseId { get; set; }
    }
}
