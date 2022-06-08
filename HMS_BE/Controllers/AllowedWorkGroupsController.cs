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
    public class AllowedWorkGroupsController : ControllerBase
    {
        private readonly HMSContext _context;

        public AllowedWorkGroupsController(HMSContext context)
        {
            _context = context;
        }

        // GET: api/AllowedWorkGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllowedWorkGroup>>> GetAllowedWorkGroups()
        {
            return await _context.AllowedWorkGroups.ToListAsync();
        }

        // GET: api/AllowedWorkGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllowedWorkGroup>> GetAllowedWorkGroup(int id)
        {
            var allowedWorkGroup = await _context.AllowedWorkGroups.FindAsync(id);

            if (allowedWorkGroup == null)
            {
                return NotFound();
            }

            return allowedWorkGroup;
        }

        // PUT: api/AllowedWorkGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAllowedWorkGroup(int id, AllowedWorkGroup allowedWorkGroup)
        {
            if (id != allowedWorkGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(allowedWorkGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllowedWorkGroupExists(id))
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

        // POST: api/AllowedWorkGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AllowedWorkGroup>> PostAllowedWorkGroup(AllowedWorkGroup allowedWorkGroup)
        {
            _context.AllowedWorkGroups.Add(allowedWorkGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllowedWorkGroup", new { id = allowedWorkGroup.Id }, allowedWorkGroup);
        }

        // DELETE: api/AllowedWorkGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllowedWorkGroup(int id)
        {
            var allowedWorkGroup = await _context.AllowedWorkGroups.FindAsync(id);
            if (allowedWorkGroup == null)
            {
                return NotFound();
            }

            _context.AllowedWorkGroups.Remove(allowedWorkGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AllowedWorkGroupExists(int id)
        {
            return _context.AllowedWorkGroups.Any(e => e.Id == id);
        }
    }
}
