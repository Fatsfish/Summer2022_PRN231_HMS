using AutoMapper;
using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using HMS_BE.Models;
using HMS_BE.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTicketsController : ControllerBase
    {
        private readonly HMSContext _context;
        private readonly IMapper _mapper;
        private readonly IWorkTicketRepository _workTicketRepository;

        public WorkTicketsController(IWorkTicketRepository workTicketRepository)
        {
            _workTicketRepository = workTicketRepository;
        }

        // GET: api/WorkTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTicket>>> GetWorkTicketStatus([FromQuery] bool isDone)
        {
            if (isDone)
            {
                var availableList = await _workTicketRepository.GetDoneWorkTickets();
                return Ok(availableList);
            }
            var list = await _workTicketRepository.GetDoneWorkTickets();
            return Ok(list);
        }

        public async Task<ActionResult<IEnumerable<GroupUser>>> GetWorkTickets([FromQuery] WorkTicketSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = HMS_BE.Utils.PagingUtil.checkDefaultPaging(paging);
                var groups = await _workTicketRepository.GetWorkTickets(searchModel, paging);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/WorkTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTicket>> GetWorkTicket(int id)
        {
            var work = await _context.WorkTickets.FindAsync(id);

            if (work == null)
            {
                return NotFound();
            }

            return work;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkTicket(int id, HMS_BE.DTO.WorkTicket workTicket)
        {
            if (id != workTicket.Id)
            {
                return BadRequest();
            }

            try
            {
                await _workTicketRepository.UpdateWorkTicket(workTicket);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _workTicketRepository.GetWorkTicketsByUserID(workTicket.Id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<HMS_BE.DTO.WorkTicket>> PostWorkTicket(HMS_BE.DTO.WorkTicket work)
        {
            try
            {
                await _workTicketRepository.AddWorkTicket(work);
            }
            catch (DbUpdateException)
            {
                if (await _workTicketRepository.GetWorkTicketsByUserID(work.Id) != null)
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetWorkTicket", new { id = work.Id }, work);
        }

        // DELETE: api/WorkTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkTicket(int id)
        {
            var group = await _workTicketRepository.GetWorkTicketsByUserID(id);
            if (group == null)
            {
                return NotFound();
            }

            var canLeave = await _workTicketRepository.CanLeaveGroup(id);
            if (canLeave)
            {
                return Conflict(new HMS_BE.DTO.Error { Message = "You must finish all work tickets to leave the group" });
            }

            await _workTicketRepository.DeleteWorkTicket(id);
            return NoContent();
        }

        private bool WorkTicketExists(int id)
        {
            return _context.WorkTickets.Any(e => e.OwnerId == id);
        }
    }
}
