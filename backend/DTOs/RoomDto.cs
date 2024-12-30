using System.ComponentModel.DataAnnotations;
namespace backend.DTOS
{
    public class RoomDto
    {
        [Required]
        public int Capacity { get; set; }
    }

}
