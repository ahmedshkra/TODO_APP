using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDO.Data;
using ToDO.Model;

namespace ToDO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class toDoesController : ControllerBase
    {
        private readonly ToDOContext _context;

        public toDoesController(ToDOContext context)
        {
            _context = context;
        }

        // GET: api/toDoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<toDo>>> GettoDo()
        {
          if (_context.toDo == null)
          {
              return NotFound();
          }
            return await _context.toDo.ToListAsync();
        }

        // GET: api/toDoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<toDo>> GettoDo(long id)
        {
          if (_context.toDo == null)
          {
              return NotFound();
          }
            var toDo = await _context.toDo.FindAsync(id);

            if (toDo == null)
            {
                return NotFound();
            }

            return toDo;
        }

        // PUT: api/toDoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttoDo(long id, toDo toDo)
        {
            if (id != toDo.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!toDoExists(id))
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

        // POST: api/toDoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<toDo>> PosttoDo(toDo toDo)
        {
          if (_context.toDo == null)
          {
              return Problem("Entity set 'ToDOContext.toDo'  is null.");
          }
            _context.toDo.Add(toDo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettoDo", new { id = toDo.Id }, toDo);
        }

        // DELETE: api/toDoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetoDo(long id)
        {
            if (_context.toDo == null)
            {
                return NotFound();
            }
            var toDo = await _context.toDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.toDo.Remove(toDo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool toDoExists(long id)
        {
            return (_context.toDo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
