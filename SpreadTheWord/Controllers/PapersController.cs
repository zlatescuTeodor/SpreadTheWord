using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpreadTheWord.Models;

namespace SpreadTheWord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PapersController : ControllerBase
    {
        private readonly PaperContext _context;

        public PapersController(PaperContext context)
        {
            _context = context;
        }

        // GET: api/Papers
        [HttpGet]
        public IEnumerable<Paper> GetPapers()
        {
            return _context.Papers;
        }

        // GET: api/Papers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaper([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paper = await _context.Papers.FindAsync(id);

            if (paper == null)
            {
                return NotFound();
            }

            return Ok(paper);
        }

        // PUT: api/Papers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaper([FromRoute] Guid id, [FromBody] Paper paper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paper.PaperId)
            {
                return BadRequest();
            }

            _context.Entry(paper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaperExists(id))
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

        // POST: api/Papers
        [HttpPost]
        public async Task<IActionResult> PostPaper([FromBody] Paper paper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Papers.Add(paper);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaper", new { id = paper.PaperId }, paper);
        }

        // DELETE: api/Papers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaper([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paper = await _context.Papers.FindAsync(id);
            if (paper == null)
            {
                return NotFound();
            }

            _context.Papers.Remove(paper);
            await _context.SaveChangesAsync();

            return Ok(paper);
        }

        private bool PaperExists(Guid id)
        {
            return _context.Papers.Any(e => e.PaperId == id);
        }
    }
}