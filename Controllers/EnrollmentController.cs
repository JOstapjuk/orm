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
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Enrollment>> GetAll()
        {
            return _context.Enrollments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Enrollment> Get(int id)
        {
            var enrollment = _context.Enrollments
                .FirstOrDefault(e => e.Id == id);
            if (enrollment == null) return NotFound();
            return enrollment;
        }

        [HttpPost]
        public ActionResult<Enrollment> Create(Enrollment dto)
        {
            // Optionally validate existence
            var student = _context.Students.Find(dto.StudentId);
            if (student == null) return BadRequest("Student not found");

            var course = _context.Courses.Find(dto.CourseId);
            if (course == null) return BadRequest("Course not found");

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrolledAt = dto.EnrolledAt
            };

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = enrollment.Id }, enrollment);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var enrollment = _context.Enrollments.Find(id);
            if (enrollment == null) return NotFound();

            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
