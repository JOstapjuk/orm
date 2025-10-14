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
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Course>> GetAll()
        {
            return _context.Courses
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> Get(int id)
        {
            var course = _context.Courses
                .FirstOrDefault(c => c.Id == id);

            if (course == null) return NotFound();
            return course;
        }

        [HttpPost]
        public ActionResult<Course> Create(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Course course)
        {
            if (id != course.Id) return BadRequest();

            _context.Entry(course).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
