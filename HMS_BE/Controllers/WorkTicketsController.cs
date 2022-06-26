using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS_BE.DTO;
using HMS_BE.Repository;
using AutoMapper;

namespace HMS_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTicketsController : ControllerBase
    {
        private readonly HMS_BE.Models.HMSContext _context;
        private readonly IWorkTicketRepository _workTicketRepository;

        public WorkTicketsController(IWorkTicketRepository workTicketRepository)
        {
            _workTicketRepository = workTicketRepository;
        }

        // GET: api/WorkTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTicket>>> GetWorkTickets([FromQuery]bool isDone)
        {
            if (isDone)
            {
                var availableList = await _workTicketRepository.GetDoneWorkTickets();
                return Ok(availableList);
            }
            var list = await _workTicketRepository.GetDoneWorkTickets();
            return Ok(list);
        }

        // GET: api/WorkTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTicket>> GetWorkTicket(int id)
        {
            var workTicket = _workTicketRepository.GetWorkTicketsByUserID(id);
            //var workTicket = await _context.WorkTickets.FindAsync(id);

            if (workTicket == null)
            {
                return NotFound();
            }

            return Ok(workTicket);
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

            //_context.Entry(workTicket).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                await _workTicketRepository.UpdateWorkTicket(workTicket);
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
            //_context.WorkTickets.Add(workTicket);
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (WorkTicketExists(workTicket.OwnerId))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            {
                await _workTicketRepository.AddWorkTicket(workTicket);

                return CreatedAtAction("GetWorkTicket", new { id = workTicket.OwnerId }, workTicket);
            }
        }

        // DELETE: api/WorkTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkTicket(int id)
        {
            //var workTicket = await _context.WorkTickets.FindAsync(id);
            //if (workTicket == null)
            //{
            //    return NotFound();
            //}

            //_context.WorkTickets.Remove(workTicket);
            //await _context.SaveChangesAsync();
            await _workTicketRepository.DeleteWorkTicket(id);

            return NoContent();

        }

        private bool WorkTicketExists(int id)
        {
            return _context.WorkTickets.Any(e => e.OwnerId == id);
        }
    }
}
