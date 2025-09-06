using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    /*
    Controlador de Autores
     - Proporciona endpoints para gestionar autores en la base de datos.
     - GET /api/autores: Obtiene una lista de todos los autores.
     - GET /api/autores/{id}: Obtiene un autor específico por su ID, incluyendo sus libros asociados.
     - POST /api/autores: Crea un nuevo autor.
     - PUT /api/autores/{id}: Actualiza un autor existente.
     - DELETE /api/autores/{id}: Elimina un autor por su ID.
    */


    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }


        // GET: api/autores
        [HttpGet]
        public async Task<IEnumerable<Autor>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        // GET: api/autores/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await context.Autores
                .Include(x => x.Libros)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (autor is null)
            {
                return NotFound();
            }
            return autor;
        }

        // POST: api/autores
        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        // PUT: api/autores/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest("Los Ids deden de coincidir");
            }
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/autores/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Autores.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
