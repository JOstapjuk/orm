using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using orm.Models;
using orm.Data;
using System.Collections.Generic;
using System.Linq;

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

        // GET: api/enrollment
        [HttpGet]
        public ActionResult<List<Enrollment>> GetAll()
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Include(e => e.Teacher)
                .ToList();

            return enrollments;
        }

        // GET: api/enrollment/{id}
        [HttpGet("{id}")]
        public ActionResult<Enrollment> Get(int id)
        {
            var enrollment = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Include(e => e.Teacher)
                .FirstOrDefault(e => e.Id == id);

            if (enrollment == null)
                return NotFound();

            return enrollment;
        }

        // POST: api/enrollment
        [HttpPost]
        [HttpPost]
        public ActionResult<Enrollment> Create([FromBody] Enrollment enrollment)
        {
            if (enrollment.Student == null || enrollment.Course == null || enrollment.Teacher == null)
                return BadRequest("Student, Course, and Teacher objects are required.");

            var student = _context.Students.Find(enrollment.Student.Id);
            var course = _context.Courses
                .Include(c => c.Classroom)
                .FirstOrDefault(c => c.Id == enrollment.Course.Id);
            var teacher = _context.Teachers.Find(enrollment.Teacher.Id);

            if (student == null) return BadRequest("Student not found");
            if (course == null) return BadRequest("Course not found");
            if (teacher == null) return BadRequest("Teacher not found");

            enrollment.Student = student;
            enrollment.Course = course;
            enrollment.Teacher = teacher;

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            //!!!!
            var result = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Classroom)
                .Include(e => e.Teacher)
                .FirstOrDefault(e => e.Id == enrollment.Id);

            return CreatedAtAction(nameof(Get), new { id = enrollment.Id }, result);
        }

        // DELETE: api/enrollment/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var enrollment = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Include(e => e.Teacher)
                .FirstOrDefault(e => e.Id == id);

            if (enrollment == null)
                return NotFound();

            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
