using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peliculaAPI.Data;
using peliculaAPI.Models;

namespace peliculaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly peliculaAPIContext _context;

        public PeliculasController(peliculaAPIContext context)
        {
            _context = context;
        }

        // GET: api/Peliculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pelicula>>> GetPelicula()
        {
            string StoredProc = "exec VerPelicula @id= null";
            return await _context.Pelicula.FromSqlRaw(StoredProc).ToListAsync();
        }

        // GET: api/Peliculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Pelicula>>> GetPelicula(int id)
        {
            string StoredProc = "exec VerPelicula " +
                 "@id = " + id + "";
            var pelicula = await _context.Pelicula.FromSqlRaw(StoredProc).ToListAsync();
            if (pelicula.Count == 0)
            {
                return NotFound("Pelicula no existe");
            }
            return pelicula;
        }

        // PUT: api/Peliculas/
        [HttpPut]
        public async Task<ActionResult<Pelicula>> PutPelicula( Pelicula pelicula)
        {
            string StoredProc = "exec VerPelicula " +
                 "@id = " + pelicula.Pel_Id + "";
            var peli = await _context.Pelicula.FromSqlRaw(StoredProc).ToListAsync();
            if (peli.Count == 0)
            {
                return NotFound("Pelicula no existe");
            }
            StoredProc = "exec ActualizarPelicula "+
                "@Id ="+pelicula.Pel_Id+","+
                "@Nombre = '" + pelicula.Pel_Nombre + "'," +
                "@Duracion = " + pelicula.Pel_Duracion + "," +
                "@Genero = " + pelicula.Pel_Genero + "," +
                "@Director = '" + pelicula.Pel_Director + "'," +
                "@Resumen = '" + pelicula.Pel_Resumen + "'";
            _context.Database.ExecuteSqlRaw(StoredProc);
                return Ok("Registro Actulizado");

        }

        // POST: api/Peliculas
        [HttpPost]
        public async Task<ActionResult> PostPelicula(CreatePelicula pelicula)
        {
            string StoredProc = "exec InsertarPelicula " +
                "@Nombre = '" + pelicula.Pel_Nombre + "'," +
                "@Duracion = " + pelicula.Pel_Duracion + ","+
                "@Genero = " + pelicula.Pel_Genero + "," +
                "@Director = '" + pelicula.Pel_Director + "'," +
                "@Resumen = '" + pelicula.Pel_Resumen + "'" ;
            try { _context.Database.ExecuteSqlRaw(StoredProc);
                
            } catch { throw; 
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPelicula", pelicula);
        }

        // DELETE: api/Peliculas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            string StoredProc = "exec VerPelicula " +
                 "@id = " + id + "";
            var peli = await _context.Pelicula.FromSqlRaw(StoredProc).ToListAsync();
            if (peli.Count == 0)
            {
                return NotFound("Pelicula no existe");
            }
            StoredProc = "exec BorrarPelicula "+
                "@id = " + id + "";
            _context.Database.ExecuteSqlRaw(StoredProc);
            return Ok("Registro Eliminado");
        }
    }
}
