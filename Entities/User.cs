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
        public string Contrasegna { get; set; }


        [Required]
        public string Correo { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public long Id_rol { get; set; }

        [ForeignKey("Id_rol")]
        public virtual Rol rol { get; set; }

    }
}
