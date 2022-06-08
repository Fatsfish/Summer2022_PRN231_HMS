using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS_BE.Models;

namespace HMS_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadersController : ControllerBase
    {
        private readonly HMSContext _context;

        public LeadersController(HMSContext context)
        {
            _context = context;
        }

        // GET: api/Leaders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leader>>> GetLeaders()
        {
            return await _context.Leaders.ToListAsync();
        }

        // GET: api/Leaders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Leader>> GetLeader(int id)
        {
            var leader = await _context.Leaders.FindAsync(id);

            if (leader == null)
            {
                return NotFound();
            }

            return leader;
        }

        // PUT: api/Leaders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeader(int id, Leader leader)
        {
            if (id != leader.Id)
            {
                return BadRequest();
            }

            _context.Entry(leader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaderExists(id))
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

        // POST: api/Leaders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Leader>> PostLeader(Leader leader)
        {
            _context.Leaders.Add(leader);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeader", new { id = leader.Id }, leader);
        }

        // DELETE: api/Leaders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeader(int id)
        {
            var leader = await _context.Leaders.FindAsync(id);
            if (leader == null)
            {
                return NotFound();
            }

            _context.Leaders.Remove(leader);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaderExists(int id)
        {
            return _context.Leaders.Any(e => e.Id == id);
        }
    }
}
