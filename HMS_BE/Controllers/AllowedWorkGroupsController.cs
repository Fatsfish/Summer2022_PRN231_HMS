using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS_BE.Models;
using HMS_BE.DTO;
using AutoMapper;
using HMS_BE.DTO.SearchModel;
using HMS_BE.DTO.PagingModel;

namespace HMS_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowedWorkGroupsController : ControllerBase
    {
        private readonly HMSContext _context;
        private readonly IMapper _mapper;
        private readonly HMS_BE.Repository.IAllowedWorkGroupRepository _allowedWorkGroupRepository;
        public AllowedWorkGroupsController(HMS_BE.Repository.IAllowedWorkGroupRepository allowedWorkGroupRepository)
        {
            _allowedWorkGroupRepository = allowedWorkGroupRepository;
        }

        // GET: api/AllowedWorkGroups
        [HttpGet]
        public async Task<IActionResult> GetAllowedWorkGroups([FromQuery] AllowedWorkGroupSearchModel searchModel, [FromQuery]PagingModel paging)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = HMS_BE.Utils.PagingUtil.checkDefaultPaging(paging);
                var groups = await _allowedWorkGroupRepository.GetAllowedWorkGroupsByGroupID(searchModel, paging);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        //// GET: api/AllowedWorkGroups/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<HMS_BE.DTO.AllowedWorkGroup>> GetAllowedWorkGroup(int id)
        //{
        //    var allowedWorkGroup = await _context.AllowedWorkGroups.FindAsync(id);

        //    var t = _mapper.Map<HMS_BE.DTO.AllowedWorkGroup>(allowedWorkGroup);

        //    if (allowedWorkGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    return t;
        //}

        //// PUT: api/AllowedWorkGroups/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAllowedWorkGroup(int id, HMS_BE.DTO.AllowedWorkGroup allowedWorkGroup)
        //{
        //    if (id != allowedWorkGroup.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var t = _mapper.Map<HMS_BE.Models.AllowedWorkGroup>(allowedWorkGroup);
        //    _context.Entry(t).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AllowedWorkGroupExists(id))
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

        //// POST: api/AllowedWorkGroups
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<HMS_BE.DTO.AllowedWorkGroup>> PostAllowedWorkGroup(HMS_BE.DTO.AllowedWorkGroup allowedWorkGroup)
        //{
        //    var t = _mapper.Map<HMS_BE.Models.AllowedWorkGroup>(allowedWorkGroup);
        //    _context.AllowedWorkGroups.Add(t);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAllowedWorkGroup", new { id = allowedWorkGroup.Id }, allowedWorkGroup);
        //}

        //// DELETE: api/AllowedWorkGroups/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAllowedWorkGroup(int id)
        //{
        //    var allowedWorkGroup = await _context.AllowedWorkGroups.FindAsync(id);
        //    if (allowedWorkGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.AllowedWorkGroups.Remove(allowedWorkGroup);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool AllowedWorkGroupExists(int id)
        //{
        //    return _context.AllowedWorkGroups.Any(e => e.Id == id);
        //}
    }
}
