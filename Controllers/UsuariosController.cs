using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase, IGenericCRUD<UsuarioDTO>
    {
        protected UsuarioRepository _repository;
        protected RolRepository _rolRepository;
        protected IMapper _mapper;
        public IConfiguration _configuration;

        public UsuariosController(UsuarioRepository repository, IMapper mapper, RolRepository rol, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _rolRepository = rol;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioDTO entity)
        {
            if (await EmailExist(entity.Correo)) return BadRequest("El correo ya está en uso");

            entity.Contrasegna = BCrypt.Net.BCrypt.HashPassword(entity.Contrasegna); // Encriptacion de la contraseña

            var usuario = await _repository.Create(_mapper.Map<Usuario>(entity));
            usuario.rol = await _rolRepository.Get(item => item.Id == usuario.Id_rol);
            return Ok(_mapper.Map<UsuarioResponseDTO>(usuario));
        }
        [HttpDelete]
        public async Task Delete(long idEntity)
        {
            await _repository.Delete(idEntity);
        }

        [HttpPut]
        public async Task Update(UsuarioDTO entity)
        {
            await _repository.Update(_mapper.Map<Usuario>(entity));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Usuario usuario = await _repository.Get(item => item.Id == id);
            usuario.rol = await _rolRepository.Get(item => item.Id == usuario.Id_rol);
            return Ok(_mapper.Map<UsuarioResponseDTO>(usuario));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Usuario> usuarios = await _repository.GetAll();
            foreach (var usuario in usuarios)
            {
                usuario.rol = await _rolRepository.Get(item => item.Id == usuario.Id_rol);
            }
            return Ok(_mapper.Map<IEnumerable<UsuarioResponseDTO>>(usuarios.ToList()));
        }

        [HttpPost("loging")]
        public async Task<IActionResult> Loging(UsuarioLoginDTO entity)
        {


            Usuario usuario = await _repository.Get(item => item.Correo == entity.Correo);

            bool verified = BCrypt.Net.BCrypt.Verify(entity.Contrasegna, usuario.Contrasegna);
            if (!verified) return BadRequest("La contraseña es incorrecta");
            if (usuario == null) return BadRequest("Correo invalido");

            usuario.rol = await _rolRepository.Get(item => item.Id == usuario.Id_rol);

            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", usuario.Id.ToString()),
                        new Claim("DisplayName", $"{usuario.Nombres} {usuario.Apellidos}"),
                        new Claim("UserName", usuario.Nombres),
                        new Claim("Rol", usuario.rol.Rol_type),
                        new Claim("Email", usuario.Correo)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        private async Task<bool> EmailExist(string email)
        {
            Usuario usuario = await _repository.Get(item => item.Correo == email);
            return usuario != null;
        }

    }
}
