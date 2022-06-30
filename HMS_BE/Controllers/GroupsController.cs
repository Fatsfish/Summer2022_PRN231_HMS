using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS_BE.Models.PagingModel;
using HMS_BE.Models.SearchModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly HMS_BE.Repository.IGroupRepository _groupRepository;

        public PagingModel PagingUltil { get; private set; }

        public GroupsController(HMS_BE.Repository.IGroupRepository grouprepository)
        {
            _groupRepository = grouprepository;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<IActionResult> GetGroups([FromQuery] GroupSearchModel searchModel, [FromQuery] PagingModel paging)
        {
            if(searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            try
            {
                paging = HMS_BE.Utils.PagingUtil.checkDefaultPaging(paging);
                var users = await _groupRepository.GetGroupList(searchModel, paging);
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HMS_BE.DTO.Group>> GetGroup(int id)
        {
            var group = await _groupRepository.GetGroupById(id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, HMS_BE.DTO.Group group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            try
            {
                await _groupRepository.UpdateGroup(group);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _groupRepository.GetGroupById(group.Id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HMS_BE.DTO.Group>> PostGroup(HMS_BE.DTO.Group group)
        {
            try
            {
                await _groupRepository.AddGroup(group);
            }
            catch (DbUpdateException)
            {
                if (await _groupRepository.GetGroupById(group.Id) != null)
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetGroup", new { id = group.Id }, group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _groupRepository.GetGroupById(id);
            if (group == null)
            {
                return NotFound();
            }

            await _groupRepository.DeleteGroup(id);
            return NoContent();
        }
    }
}
