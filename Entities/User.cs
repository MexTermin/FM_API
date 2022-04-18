using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FM_API.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Pass { get; set; }

        [Required]
        public string Email { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public long Id_rol { get; set; }

        [ForeignKey("Id_rol")]
        public virtual Rol Rol { get; set; }

    }
}
