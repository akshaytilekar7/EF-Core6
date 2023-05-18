#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly PubContext _context;

        public AuthorsController(PubContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            return await _context.Authors
               .Select(a => new AuthorDTO
               {
                   AuthorId = a.Id,
                   FirstName = a.FirstName,
                   LastName = a.LastName
               })
               .ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return AuthorToDTO(author);
        }
        private static AuthorDTO AuthorToDTO(Author author)
        {
            return new AuthorDTO
            {
                AuthorId = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDTO authorDTO)
        {
            if (id != authorDTO.AuthorId)
            {
                return BadRequest();
            }

            Author author = AuthorFromDTO(authorDTO);
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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



        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDTO authorDTO)
        {
            var author = AuthorFromDTO(authorDTO);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            //var author = await _context.Authors.FindAsync(id);
            //if (author == null)
            //{
            //    return NotFound();
            //}

            //_context.Authors.Remove(author);
            //await _context.SaveChangesAsync();

            // OR

            var rowCount = await _context.Database.ExecuteSqlInterpolatedAsync($" Delete from authors where id = {id}");
            if (rowCount == 0) return NotFound();
            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }

        private static Author AuthorFromDTO(AuthorDTO authorDTO)
        {
            return new Author
            {
                Id = authorDTO.AuthorId,
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName
            };
        }




    }
}
