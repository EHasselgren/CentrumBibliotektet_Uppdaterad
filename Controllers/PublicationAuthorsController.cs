using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentrumBibliotektet_Uppdaterad.Data;
using CentrumBibliotektet_Uppdaterad.Models;

namespace CentrumBibliotektet_Uppdaterad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationAuthorsController : ControllerBase
    {
        private readonly Context _context;

        public PublicationAuthorsController(Context context)
        {
            _context = context;
        }

        // GET: api/PublicationAuthors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationAuthor>>> GetPublicationAuthors()
        {
            return await _context.PublicationAuthors.ToListAsync();
        }

        // GET: api/PublicationAuthors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicationAuthor>> GetPublicationAuthor(int id)
        {
            var publicationAuthor = await _context.PublicationAuthors.FindAsync(id);

            if (publicationAuthor == null)
            {
                return NotFound();
            }

            return publicationAuthor;
        }

        // PUT: api/PublicationAuthors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicationAuthor(int id, PublicationAuthor publicationAuthor)
        {
            if (id != publicationAuthor.AuthorId)
            {
                return BadRequest();
            }

            _context.Entry(publicationAuthor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationAuthorExists(id))
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

        // POST: api/PublicationAuthors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PublicationAuthor>> PostPublicationAuthor(PublicationAuthor publicationAuthor)
        {
            _context.PublicationAuthors.Add(publicationAuthor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PublicationAuthorExists(publicationAuthor.AuthorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPublicationAuthor", new { id = publicationAuthor.AuthorId }, publicationAuthor);
        }

        // DELETE: api/PublicationAuthors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicationAuthor>> DeletePublicationAuthor(int id)
        {
            var publicationAuthor = await _context.PublicationAuthors.FindAsync(id);
            if (publicationAuthor == null)
            {
                return NotFound();
            }

            _context.PublicationAuthors.Remove(publicationAuthor);
            await _context.SaveChangesAsync();

            return publicationAuthor;
        }

        private bool PublicationAuthorExists(int id)
        {
            return _context.PublicationAuthors.Any(e => e.AuthorId == id);
        }
    }
}
