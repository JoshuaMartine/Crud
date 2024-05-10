using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteControllers : ControllerBase
    {
        private readonly EstudianteData _estudianteData;

        public EstudianteControllers(EstudianteData estudianteData)
        {
            _estudianteData = estudianteData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Estudiante> lista = await _estudianteData.Lista();
            return Ok(lista); // Simplificado el uso de StatusCode para utilizar métodos más legibles de ActionResult
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(int Id)
        {
            Estudiante objeto = await _estudianteData.Obtener(Id);
            if (objeto == null)
            {
                return NotFound();
            }
            return Ok(objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Estudiante objeto)
        {
            bool respuesta = await _estudianteData.Crear(objeto);
            if (respuesta)
            {
                return Ok(new { isSuccess = true });
            }
            else
            {
                return BadRequest(new { isSuccess = false });
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(int Id, [FromBody] Estudiante objeto)
        {
            if (objeto.IdEstudiante != Id)
            {
                return BadRequest(new { isSuccess = false, message = "ID mismatch" });
            }
            bool respuesta = await _estudianteData.Editar(objeto);
            if (respuesta)
            {
                return Ok(new { isSuccess = true });
            }
            else
            {
                return BadRequest(new { isSuccess = false });
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(int Id)
        {
            bool respuesta = await _estudianteData.Eliminar(Id);
            if (respuesta)
            {
                return Ok(new { isSuccess = true });
            }
            else
            {
                return BadRequest(new { isSuccess = false });
            }
        }
    }
}
