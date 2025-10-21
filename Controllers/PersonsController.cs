using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using orm.Data;
using orm.Models;

namespace orm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Set<Person>()
                .Include(p => p.ContactInfo)
                .Include(p => p.Documents)
                .ToListAsync();
        }

        // GET: api/persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Set<Person>()
                .Include(p => p.ContactInfo)
                .Include(p => p.Documents)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // POST: api/persons
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Set<Person>().Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
        }

        // PUT: api/persons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            if (person.ContactInfo != null)
            {
                _context.Entry(person.ContactInfo).State = person.ContactInfo.Id == 0
                    ? EntityState.Added
                    : EntityState.Modified;
            }

            if (person.Documents != null)
            {
                foreach (var doc in person.Documents)
                {
                    _context.Entry(doc).State = doc.Id == 0
                        ? EntityState.Added
                        : EntityState.Modified;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // DELETE: api/persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.Set<Person>()
                .Include(p => p.ContactInfo)
                .Include(p => p.Documents)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            if (person.ContactInfo != null)
                _context.Remove(person.ContactInfo);

            if (person.Documents != null && person.Documents.Any())
                _context.RemoveRange(person.Documents);

            _context.Set<Person>().Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Set<Person>().Any(e => e.Id == id);
        }
    }
}
