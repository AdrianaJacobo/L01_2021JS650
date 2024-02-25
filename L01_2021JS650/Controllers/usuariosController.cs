using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021JS650.Models;

namespace L01_2021JS650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly BlogDbContext _blogDbContexto;

        public usuariosController(BlogDbContext blogDbContexto)
        {
            _blogDbContexto = blogDbContexto;
        }


        //VER TODOS LOS USUARIOS (R- READ)
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Usuario> listadoUsuario = (from e in _blogDbContexto.Usuarios
                                            select e).ToList();

            if (listadoUsuario.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuario);
        }


        //CREAR USUARIO (C- CREATE)
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _blogDbContexto.Usuarios.Add(usuario);
                _blogDbContexto.SaveChanges();
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        
        

        //ACTUALIZAR USUARIO (U- UPDATE)
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] Usuario usuarioModificado)
        {
            Usuario? usuarioActual = (from e in _blogDbContexto.Usuarios
                                    where e.UsuarioId == id
                                    select e).FirstOrDefault();

            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.RolId = usuarioModificado.RolId;
            usuarioActual.NombreUsuario = usuarioModificado.NombreUsuario;
            usuarioActual.Clave = usuarioModificado.Clave;
            usuarioActual.Nombre = usuarioModificado.Nombre;
            usuarioActual.Apellido = usuarioModificado.Apellido;


            _blogDbContexto.Entry(usuarioActual).State = EntityState.Modified;
            _blogDbContexto.SaveChanges();

            return Ok(usuarioModificado);
        }

        //ELIMINAR USUARIO (D- Delete)
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarUsuario(int id)
        {
            Usuario? usuario = (from e in _blogDbContexto.Usuarios
                               where e.UsuarioId == id
                              select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }

            _blogDbContexto.Usuarios.Attach(usuario);
            _blogDbContexto.Usuarios.Remove(usuario);
            _blogDbContexto.SaveChanges();

            return Ok(usuario);

        }

        //FILTRAR POR NOMBRE Y APELLIDO
        [HttpGet]
        [Route("FindByNombre-Apellido")]

        public IActionResult FindByNombreApellido(string nombre, string apellido)
        {
            List<Usuario> listadoUsuario = (from e in _blogDbContexto.Usuarios
                                            where e.Nombre == nombre && e.Apellido == apellido
                                            select e).ToList();

            if (listadoUsuario.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuario);
        }

        //FILTRAR POR ROL
        [HttpGet]
        [Route("GetByRolID/{rolId}")]

        public IActionResult Get(int rolId)
        {
            List<Usuario> listadoUsuario = (from e in _blogDbContexto.Usuarios
                                            where e.RolId == rolId
                                            select e).ToList();

            if (listadoUsuario.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuario);
        }
    }
}
