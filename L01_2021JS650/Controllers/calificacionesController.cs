using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021JS650.Models;

namespace L01_2021JS650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly BlogDbContext _blogDbContexto;

        public calificacionesController(BlogDbContext blogDbContexto)
        {
            _blogDbContexto = blogDbContexto;
        }

        //VER TODAS LAS CALIFICACIONES (R- READ)
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Calificacione> listadoCalificaciones = (from e in _blogDbContexto.Calificaciones
                                                         select e).ToList();

            if (listadoCalificaciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoCalificaciones);
        }

        //CREAR CALIFICACIONES (C- CREATE)
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarCalificaciones([FromBody] Calificacione calificaciones)
        {
            try
            {
                _blogDbContexto.Calificaciones.Add(calificaciones);
                _blogDbContexto.SaveChanges();
                return Ok(calificaciones);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        

        //ACTUALIZAR CALIFICACIONES (U- UPDATE)
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarCalificaciones(int id, [FromBody] Calificacione calificacionModificada)
        {
            Calificacione? calificacionActual = (from e in _blogDbContexto.Calificaciones
                                      where e.CalificacionId == id
                                      select e).FirstOrDefault();

            if (calificacionActual == null)
            {
                return NotFound();
            }

            calificacionActual.PublicacionId = calificacionModificada.PublicacionId;
            calificacionActual.UsuarioId = calificacionModificada.UsuarioId;
            calificacionActual.Calificacion = calificacionModificada.Calificacion;



            _blogDbContexto.Entry(calificacionActual).State = EntityState.Modified;
            _blogDbContexto.SaveChanges();

            return Ok(calificacionModificada);
        }

        //ELIMINAR CALIFICACION (D- Delete)
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarCalificacion(int id)
        {
            Calificacione? calificaciones = (from e in _blogDbContexto.Calificaciones
                                where e.CalificacionId == id
                                select e).FirstOrDefault();

            if (calificaciones == null)
            {
                return NotFound();
            }

            _blogDbContexto.Calificaciones.Attach(calificaciones);
            _blogDbContexto.Calificaciones.Remove(calificaciones);
            _blogDbContexto.SaveChanges();

            return Ok(calificaciones);

        }

        //FILTRAR POR PUBLICACION (calificaciones filtradas por una publicación en específico.)
        [HttpGet]
        [Route("GetByPublicacionID/{publicacionId}")]

        public IActionResult Get(int publicacionId)
        {
            List<Calificacione> listadoCalifxPubli = (from e in _blogDbContexto.Calificaciones
                                            where e.PublicacionId == publicacionId
                                            select e).ToList();

            if (listadoCalifxPubli.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoCalifxPubli);
        }


    }
}
