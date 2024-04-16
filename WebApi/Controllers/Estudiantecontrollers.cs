using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApi.Data;
using WebApi.Controllers;
using WebApi.models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Estudiantecontrollers : ControllerBase
    {

        private readonly EstudianteData _estudianteData;

        public Estudiantecontrollers(EstudianteData estudianteData)
        {
            _estudianteData = estudianteData;
        }

        [HttpGet]
     
        public async Task<IActionResult> Lista()
        {
           List <Estudiante>Lista = await  _estudianteData.Lista();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }


        [HttpGet("{Id}")]

        public async Task<IActionResult> Obtener(int Id)
        {
            Estudiante Objeto = await _estudianteData.Obtener(Id);

            return StatusCode(StatusCodes.Status200OK, Objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Estudiante objeto)
        {
            bool respuesta = await _estudianteData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }



        [HttpPut]

        public async Task<IActionResult> Editar([FromBody] Estudiante Objeto)
        {
            bool repuesta = await _estudianteData.Editar(Objeto);

            return StatusCode(StatusCodes.Status200OK, new { isSucces = repuesta });
        }


        [HttpDelete("{Id}")]

        public async Task<IActionResult> Eliminar(int Id)
        {
            bool repuesta = await _estudianteData.Eliminar(Id);

            return StatusCode(StatusCodes.Status200OK, new { isSucces = repuesta });
        }
    }
}
