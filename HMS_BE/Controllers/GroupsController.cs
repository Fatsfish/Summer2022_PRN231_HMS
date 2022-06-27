using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly HMS_BE.Repository.IGroupRepository repo;

        public GroupsController(HMS_BE.Repository.IGroupRepository repository)
        {
            repo = repository;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HMS_BE.DTO.Group>>> GetGroups()
        {
            var list = await repo.GetGroupList();
            if(list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("/AvailabelGroup")]
        public async Task<ActionResult<IEnumerable<HMS_BE.DTO.Group>>> GetAvailableGroups()
        {
            var list = await repo.GetAvalableGroupList();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HMS_BE.DTO.Group>> GetGroup(int id)
        {
            var group = await repo.GetGroupById(id);

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
                await repo.UpdateGroup(group);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await repo.GetGroupById(group.Id) == null)
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
                await repo.AddGroup(group);
            }
            catch (DbUpdateException)
            {
                if (await repo.GetGroupById(group.Id) != null)
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
            var group = await repo.GetGroupById(id);
            if (group == null)
            {
                return NotFound();
            }

            await repo.DeleteGroup(id);
            return NoContent();
        }
    }
}
