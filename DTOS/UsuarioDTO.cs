#nullable disable

namespace FM_API.DTOS
{
    public class UsuarioDTO
    {
        public long Id { get; set; }

        public string Contrasegna { get; set; }

        public string Correo { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public long Id_rol { get; set; }

        public RolDTO rol { get; set; }
    }

    public class UsuarioLoginDTO
    {
        public string Correo { get; set; }
        public string Contrasegna { get; set; }
    }

    public class UsuarioResponseDTO
    {
        public long Id { get; set; }

        public string Correo { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public RolDTO rol { get; set; }
    }
}
