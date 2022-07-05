using AutoMapper;
using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using HMS_BE.Models;
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
    public class WorksController : ControllerBase
    {
        private readonly HMSContext _context;
        private readonly IMapper _mapper;
        private readonly HMS_BE.Repository.IWorkRepository _workRepository;
        private readonly HMS_BE.Repository.IAllowedWorkGroupRepository _allowedWorkGroupRepository;

        public WorksController(HMS_BE.Repository.IWorkRepository workRepository, HMS_BE.Repository.IAllowedWorkGroupRepository allowedWorkGroupRepository)
        {
            _workRepository = workRepository;
            _allowedWorkGroupRepository = allowedWorkGroupRepository;
        }

        // GET: api/Works
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Work>>> GetWorks()
        //{
        //    return await _context.Works.ToListAsync();
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTO.Work>>> GetWorks([FromQuery] WorkSearchModel workSearchModel, PagingModel paging)
        {
            if (workSearchModel is null)
            {
                throw new ArgumentNullException(nameof(workSearchModel));
            }

            try
            {
                paging = HMS_BE.Utils.PagingUtil.checkDefaultPaging(paging);
                var works = await _workRepository.GetWorks(workSearchModel, paging);
                return Ok(works);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        //GET: api/Works
        //GET work by group id

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DTO.Work>>> GetWork([FromQuery] AllowedWorkGroupSearchModel wrgSearchModel, PagingModel paging)
        {
            var wgrlist = await _allowedWorkGroupRepository.GetAllowedWorkGroupsByGroupID(wrgSearchModel, paging);

            if (wgrlist == null)
            {
                return NotFound();
            }
            List<DTO.Work> wlist = new List<DTO.Work>();
            foreach (var wgr in wgrlist.Data)
            {
                var w = await _workRepository.GetWorkById(wgr.Work.Id);
                wlist.Add(w);
            }
            return Ok(wlist.AsEnumerable());
        }
        //// GET: api/Works/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Work>> GetWork(int id)
        {
            var work = await _context.Works.FindAsync(id);

            if (work == null)
            {
                return NotFound();
            }

            return work;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWork(int id, HMS_BE.DTO.Work work)
        {
            if (id != work.Id)
            {
                return BadRequest();
            }

            try
            {
                await _workRepository.UpdateWork(work);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _workRepository.GetWorkById(work.Id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<HMS_BE.DTO.Work>> PostWork(HMS_BE.DTO.Work work)
        {
            try
            {
                await _workRepository.AddWork(work);
            }
            catch (DbUpdateException)
            {
                if (await _workRepository.GetWorkById(work.Id) != null)
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetWork", new { id = work.Id }, work);
        }

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

        // PUT: api/Works/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // POST: api/Works
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // DELETE: api/Works/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork(int id)
        {
            var group = await _workRepository.GetWorkById(id);
            if (group == null)
            {
                return NotFound();
            }

            await _workRepository.DeleteWork(id);
            return NoContent();
        }

    }
}
