#nullable disable

namespace FM_API.DTOS
{
    public class UserDTO
    {
        public long Id { get; set; }

        public string Pass { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public long Id_rol { get; set; }

        public RolDTO Rol { get; set; }
    }

    public class UsuarioLoginDTO
    {
        public string Email { get; set; }
        public string Pass { get; set; }
    }

    public class UsuarioResponseDTO
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public RolDTO Rol { get; set; }
    }
}
