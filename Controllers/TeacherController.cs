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
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Teacher>> GetAll()
        {
            return _context.Teachers.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Teacher> Get(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null) return NotFound();
            return teacher;
        }

        [HttpPost]
        public ActionResult<Teacher> Create(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = teacher.Id }, teacher);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Teacher teacher)
        {
            if (id != teacher.Id) return BadRequest();

            _context.Entry(teacher).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null) return NotFound();

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
