using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS_BE.DTO;

namespace HMS_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly HMS_BE.Repository.IWorkRepository _workRepository;
        private readonly HMS_BE.Repository.IAllowedWorkGroupRepository _allowedWorkGroupRepository;

        public WorksController(HMS_BE.Repository.IWorkRepository workRepository, HMS_BE.Repository.IAllowedWorkGroupRepository allowedWorkGroupRepository)
        {
            _workRepository = workRepository;
            _allowedWorkGroupRepository = allowedWorkGroupRepository;
        }

        //// GET: api/Works
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Work>>> GetWorks()
        //{
        //    return await _context.Works.ToListAsync();
        //}

        // GET: api/Works
        // GET work by group id
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Work>>> GetWorks([System.Web.Http.FromUri] int groupId)
        {
            var wgrlist = await _allowedWorkGroupRepository.GetAllowedWorkGroupsByGroupID(groupId);
            
            if (wgrlist == null)
            {
                return NotFound();
            }
            List<Work> wlist = new List<Work>();
            foreach (var wgr in wgrlist)
                {
                    var w = await _workRepository.GetWorkById((int)wgr.WorkId);
                    wlist.Add(w);
                }
            return Ok(wlist.AsEnumerable());
        }

        //// GET: api/Works/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Work>> GetWork(int id)
        //{
        //    var work = await _context.Works.FindAsync(id);

        //    if (work == null)
        //    {
        //        return NotFound();
        //    }

        //    return work;
        //}

        //// PUT: api/Works/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWork(int id, Work work)
        //{
        //    if (id != work.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(work).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WorkExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Works
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Work>> PostWork(Work work)
        //{
        //    _context.Works.Add(work);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetWork", new { id = work.Id }, work);
        //}

        //// DELETE: api/Works/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWork(int id)
        //{
        //    var work = await _context.Works.FindAsync(id);
        //    if (work == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Works.Remove(work);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
