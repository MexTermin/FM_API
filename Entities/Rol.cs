using System.ComponentModel.DataAnnotations;
#nullable disable

namespace FM_API.Entities
{
    public class Rol
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Rol_type { get; set; }

    }
}
