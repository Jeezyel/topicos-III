using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A1.Data;
using A1.Models;

namespace A1.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCardapioController : ControllerBase
    {
        private readonly A1Context _context;

        public ItemCardapioController(A1Context context)
        {
            _context = context;
        }

        // GET: api/ItemCardapio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCardapio>>> GetItemCardapio()
        {
            return await _context.ItemCardapio.ToListAsync();
        }

        // GET: api/ItemCardapio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCardapio>> GetItemCardapio(int id)
        {
            var itemCardapio = await _context.ItemCardapio.FindAsync(id);

            if (itemCardapio == null)
            {
                return NotFound();
            }

            return itemCardapio;
        }

        // PUT: api/ItemCardapio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCardapio(int id, ItemCardapio itemCardapio)
        {
            if (id != itemCardapio.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemCardapio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCardapioExists(id))
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

        // POST: api/ItemCardapio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemCardapio>> PostItemCardapio(ItemCardapio itemCardapio)
        {
            _context.ItemCardapio.Add(itemCardapio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCardapio", new { id = itemCardapio.Id }, itemCardapio);
        }

        // DELETE: api/ItemCardapio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCardapio(int id)
        {
            var itemCardapio = await _context.ItemCardapio.FindAsync(id);
            if (itemCardapio == null)
            {
                return NotFound();
            }

            _context.ItemCardapio.Remove(itemCardapio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemCardapioExists(int id)
        {
            return _context.ItemCardapio.Any(e => e.Id == id);
        }
    }
}
