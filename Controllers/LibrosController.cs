using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    /*
    Controlador de Libros
     - Proporciona endpoints para gestionar libros en la base de datos.
     - GET /api/libros: Obtiene una lista de todos los libros.
     - GET /api/libros/{id}: Obtiene un libro específico por su ID, incluyendo su autor asociado.
     - POST /api/libros: Crea un nuevo libro, verificando que el autor exista.
     - PUT /api/libros/{id}: Actualiza un libro existente, verificando que el autor exista.
     - DELETE /api/libros/{id}: Elimina un libro por su ID.
    */


    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/libros
        [HttpGet]
        public async Task<IEnumerable<Libro>> Get()
        {
            return await context.Libros.ToListAsync();
        }

        // GET: api/libros/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await context.Libros
                .Include(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (libro is null)
            {
                return NotFound();
            }
            return libro;
        }

        // POST: api/libros
        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (!existeAutor)
            {
                return BadRequest($"No existe un autor con el Id {libro.AutorId}");
            }

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        // PUT: api/libros/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest("Los ids deden de coincidir");
            }
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (!existeAutor)
            {
                return BadRequest($"No existe un autor con el Id {libro.AutorId}");
            }

            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/libros/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Libros.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
