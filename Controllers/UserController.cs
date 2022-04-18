using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FMAPI.Helpers;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase, IGenericCRUD<UserDTO>
    {
        protected UserRepository _repository;
        protected RolRepository _rolRepository;
        protected IMapper _mapper;
        protected IConfiguration _configuration;

        public UserController(UserRepository repository, IMapper mapper, RolRepository rol, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _rolRepository = rol;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO entity)
        {

            try
            {
                if (await EmailExist(entity.Email)) return BadRequest("El correo ya está en uso");

                entity.Pass = BCrypt.Net.BCrypt.HashPassword(entity.Pass); // Encriptacion de la contraseña

                var usuario = await _repository.Create(_mapper.Map<User>(entity));
                usuario.Rol = await _rolRepository.Get(item => item.Id == usuario.Id_rol);
                ResponseHelper<UsuarioResponseDTO> response = new(MessageHelper.SuccessMessage.MaCreate, _mapper.Map<UsuarioResponseDTO>(usuario));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long idEntity)
        {
            try
            {
                await _repository.Delete(idEntity);
                ResponseHelper response = new(MessageHelper.SuccessMessage.MaDelete);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDTO entity)
        {
            try
            {
                if (entity.Pass != null)
                {
                    entity.Pass = BCrypt.Net.BCrypt.HashPassword(entity.Pass);
                }
              
                await _repository.UpdateUser(_mapper.Map<User>(entity));
                ResponseHelper response = new(MessageHelper.SuccessMessage.MaUpdated);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {

            try
            {
                User usuario = await _repository.Get(item => item.Id == id);
                usuario.Rol = await _rolRepository.Get(item => item.Id == usuario.Id_rol);
                ResponseHelper<UsuarioResponseDTO> response = new("", _mapper.Map<UsuarioResponseDTO>(usuario));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                IEnumerable<User> usuarios = await _repository.GetAll();
                foreach (var usuario in usuarios)
                {
                    usuario.Rol = await _rolRepository.Get(item => item.Id == usuario.Id_rol);
                }
                ResponseHelper<IEnumerable<UsuarioResponseDTO>> response = new("", _mapper.Map<IEnumerable<UsuarioResponseDTO>>(usuarios.ToList()));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDTO entity)
        {
            try
            {
                User usuario = await _repository.Get(item => item.Email == entity.Email);

                if (usuario == null) return BadRequest(new ResponseHelper("Correo ó contraseña inválido", error: true));
                bool verified = BCrypt.Net.BCrypt.Verify(entity.Pass, usuario.Pass);
                if (!verified) return BadRequest(new ResponseHelper("Correo ó contraseña inválidosss", error: true));

                usuario.Rol = await _rolRepository.Get(item => item.Id == usuario.Id_rol);

                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", usuario.Id.ToString()),
                        new Claim("DisplayName", $"{usuario.Name} {usuario.Lastname}"),
                        new Claim("UserName", usuario.Name),
                        new Claim("Rol", usuario.Rol.Rol_type),
                        new Claim("Email", usuario.Email)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddHours(8),
                    signingCredentials: signIn);
                ResponseHelper<string> response = new("", new JwtSecurityTokenHandler().WriteToken(token));

                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        private async Task<bool> EmailExist(string email)
        {
            User usuario = await _repository.Get(item => item.Email == email);
            return usuario != null;
        }

    }
}
