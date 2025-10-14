using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using orm.Data;
using orm.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace orm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Persons
                .Include(p => p.ContactInfo)
                .Include(p => p.Documents)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPersons), new { id = person.Id }, person);
        }
    }
}
