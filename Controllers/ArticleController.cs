using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using orm.Data;
using orm.Models;

namespace orm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Article> GetArticles()
        {
            var articles = _context.Articles.ToList();
            return articles;
        }

        [HttpPost("article")]
        public List<Article> PostArtikkel([FromBody] Article artikkel)
        {
            _context.Articles.Add(artikkel);
            _context.SaveChanges();
            return _context.Articles.ToList();
        }

        [HttpDelete("{id}")]
        public List<Article> DeleteArtikkel(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return _context.Articles.ToList();
            }

            _context.Articles.Remove(artikkel);
            _context.SaveChanges();
            return _context.Articles.ToList();
        }

        [HttpDelete("/kustuta2/{id}")]
        public IActionResult DeleteArtikkel2(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(artikkel);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Article> GetArtikkel(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            return artikkel;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Article>> PutArtikkel(int id, [FromBody] Article updatedArtikkel)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            artikkel.Header = updatedArtikkel.Header;
            artikkel.Content = updatedArtikkel.Content;

            _context.Articles.Update(artikkel);
            _context.SaveChanges();

            return Ok(_context.Articles);
        }

        [HttpPost("comment")]
        public ActionResult<List<Comment>> PostComment([FromBody] Comment comment)
        {
            comment.Date = DateTime.Now;
            _context.Comments.Add(comment);
            _context.SaveChanges();

            var article = _context.Articles.Include(a => a.Comments).SingleOrDefault(a => a.Id == comment.ArticleId);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article.Comments);
        }

        [HttpGet("article/{articleId}")]
        public ActionResult<List<Comment>> GetCommentsForArticle(int articleId)
        {
            var comments = _context.Comments.Where(c => c.ArticleId == articleId).ToList();

            if (comments.Count == 0)
            {
                return NotFound();
            }

            return comments;
        }

        [HttpPost("author")]
        public List<Author> PostAuthor([FromBody] Author author)
        {
            _context.ContactDatas.Add(author.Contact);
            _context.SaveChanges();

            author.ContactDataId = author.Contact.Id;

            _context.Authors.Add(author);
            _context.SaveChanges();
            return _context.Authors.Include(a => a.Contact).ToList();
        }

        [HttpGet("author")]
        public List<Author> GetAuthors()
        {
            var authors = _context.Authors.Include(a => a.Contact).ToList();
            return authors;
        }

        [HttpGet("author/{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = _context.Authors.Include(a => a.Contact).FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        [HttpDelete("author/{id}")]
        public List<Author> DeleteAuthor(int id)
        {
            var author = _context.Authors.Include(a => a.Contact).FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return _context.Authors.Include(a => a.Contact).ToList();
            }

            if (author.Contact != null)
            {
                _context.ContactDatas.Remove(author.Contact);
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return _context.Authors.Include(a => a.Contact).ToList();
        }

        [HttpPut("author/{id}")]
        public ActionResult<List<Author>> PutAuthor(int id, [FromBody] Author updatedAuthor)
        {
            var author = _context.Authors.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            author.FirstName = updatedAuthor.FirstName;
            author.LastName = updatedAuthor.LastName;
            author.PersonalCode = updatedAuthor.PersonalCode;

            var contactData = _context.ContactDatas.Find(author.ContactDataId);
            if (contactData != null)
            {
                contactData.Address = updatedAuthor.Contact.Address;
                contactData.Phone = updatedAuthor.Contact.Phone;
            }

            _context.SaveChanges();

            return Ok(_context.Authors);
        }
    }
}
