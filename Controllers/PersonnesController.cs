using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapitest;
using webapitest.Models;

namespace webapitest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnesController : ControllerBase
    {
        private readonly PersonneContext _context;

        public PersonnesController(PersonneContext context)
        {
            _context = context;
        }

        // GET: api/Personnes
        [HttpGet("getallpersones")]
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonnes()
        {
            var personnes= await _context.Personnes.ToListAsync();

            if(personnes.Count()==0)
            {
                return NotFound(new { message ="La collection des personnes est vide" });
            }

            return Ok(personnes);
        }

        // GET: api/Personnes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personne>> GetPersonne(Guid id)
        {
            var personne = await _context.Personnes.SingleOrDefaultAsync(person=> person.Id==id);

            if (personne == null)
            {
                return NotFound();
            }

            return personne;
        }

        // PUT: api/Personnes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonne(Guid id, Personne personne)
        {
            if (id != personne.Id)
            {
                return BadRequest();
            }

            _context.Entry(personne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Personnes
        [HttpPost]
        public async Task<ActionResult<Personne>> PostPersonne(Personne personne)
        {
            _context.Personnes.Add(personne);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonne", new { id = personne.Id }, personne);
        }

        // DELETE: api/Personnes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personne>> DeletePersonne(Guid id)
        {
            var personne = await _context.Personnes.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }

            _context.Personnes.Remove(personne);
            await _context.SaveChangesAsync();

            return personne;
        }

        private bool PersonneExists(Guid id)
        {
            return _context.Personnes.Any(e => e.Id == id);
        }
    }
}
