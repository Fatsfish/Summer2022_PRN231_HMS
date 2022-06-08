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
    public class WorkTicketsController : ControllerBase
    {
        private readonly HMSContext _context;

        public WorkTicketsController(HMSContext context)
        {
            _context = context;
        }

        // GET: api/WorkTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTicket>>> GetWorkTickets()
        {
            return await _context.WorkTickets.ToListAsync();
        }

        // GET: api/WorkTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTicket>> GetWorkTicket(int id)
        {
            var workTicket = await _context.WorkTickets.FindAsync(id);

            if (workTicket == null)
            {
                return NotFound();
            }

            return workTicket;
        }

        // PUT: api/WorkTickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkTicket(int id, WorkTicket workTicket)
        {
            if (id != workTicket.OwnerId)
            {
                return BadRequest();
            }

            _context.Entry(workTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTicketExists(id))
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

        // POST: api/WorkTickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkTicket>> PostWorkTicket(WorkTicket workTicket)
        {
            _context.WorkTickets.Add(workTicket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WorkTicketExists(workTicket.OwnerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWorkTicket", new { id = workTicket.OwnerId }, workTicket);
        }

        // DELETE: api/WorkTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkTicket(int id)
        {
            var workTicket = await _context.WorkTickets.FindAsync(id);
            if (workTicket == null)
            {
                return NotFound();
            }

            _context.WorkTickets.Remove(workTicket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkTicketExists(int id)
        {
            return _context.WorkTickets.Any(e => e.OwnerId == id);
        }
    }
}
