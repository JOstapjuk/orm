using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using orm.Models;
using orm.Data;
using System;

namespace orm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassroomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Classroom>> GetAll()
        {
            return _context.Classrooms.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Classroom> Get(int id)
        {
            var classroom = _context.Classrooms.FirstOrDefault(c => c.Id == id);
            if (classroom == null) return NotFound();
            return classroom;
        }

        [HttpPost]
        public ActionResult<Classroom> Create(Classroom classroom)
        {
            _context.Classrooms.Add(classroom);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = classroom.Id }, classroom);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Classroom classroom)
        {
            if (id != classroom.Id) return BadRequest();

            _context.Entry(classroom).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var classroom = _context.Classrooms.Find(id);
            if (classroom == null) return NotFound();

            _context.Classrooms.Remove(classroom);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
