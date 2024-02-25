using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021JS650.Models;

namespace L01_2021JS650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly BlogDbContext _blogDbContexto;

        public comentariosController(BlogDbContext blogDbContexto)
        {
            _blogDbContexto = blogDbContexto;
        }

        //VER TODOS LOS COMENTARIOS (R- READ)
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Comentario> listadoComentarios = (from e in _blogDbContexto.Comentarios
                                                   select e).ToList();

            if (listadoComentarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoComentarios);
        }

        //CREAR COMENTARIOS (C- CREATE)
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarComentarios([FromBody] Comentario comentarios)
        {
            try
            {
                _blogDbContexto.Comentarios.Add(comentarios);
                _blogDbContexto.SaveChanges();
                return Ok(comentarios);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        

        //ACTUALIZAR COMENTARIO (U- UPDATE)
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarComentarios(int id, [FromBody] Comentario comentarioModificado)
        {
            Comentario? comentarioActual = (from e in _blogDbContexto.Comentarios
                                            where e.CometarioId == id
                                            select e).FirstOrDefault();

            if (comentarioActual == null)
            {
                return NotFound();
            }

            comentarioActual.PublicacionId = comentarioModificado.PublicacionId;
            comentarioActual.Comentario1 = comentarioModificado.Comentario1;
            comentarioActual.UsuarioId = comentarioModificado.UsuarioId;



            _blogDbContexto.Entry(comentarioActual).State = EntityState.Modified;
            _blogDbContexto.SaveChanges();

            return Ok(comentarioModificado);
        }

        //ELIMINAR COMENTARIO (D- Delete)
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarComentario(int id)
        {
            Comentario? comentario = (from e in _blogDbContexto.Comentarios
                                      where e.CometarioId == id
                                      select e).FirstOrDefault();

            if (comentario == null)
            {
                return NotFound();
            }

            _blogDbContexto.Comentarios.Attach(comentario);
            _blogDbContexto.Comentarios.Remove(comentario);
            _blogDbContexto.SaveChanges();

            return Ok(comentario);

        }

        //FILTRAR POR USUARIO (comentarios filtrados por un usuario en específico)
        [HttpGet]
        [Route("GetByUsuarioID/{usuarioId}")]

        public IActionResult Get(int usuarioId)
        {
            List<Comentario> listadoCommentxUser = (from e in _blogDbContexto.Comentarios
                                                      where e.UsuarioId == usuarioId
                                                      select e).ToList();

            if (listadoCommentxUser.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoCommentxUser);
        }



    }
}
