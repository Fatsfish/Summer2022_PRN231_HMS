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
    public class GroupUsersController : ControllerBase
    {
        private readonly HMS_BE.Models.HMSContext _context;
        private readonly HMS_BE.Repository.IGroupUserRepository _groupUserRepository;
        private readonly HMS_BE.Repository.IWorkTicketRepository _workTicketRepository;

        public GroupUsersController(HMS_BE.Repository.IGroupUserRepository groupUserRepository, HMS_BE.Repository.IWorkTicketRepository workTicketRepository)
        {
            _groupUserRepository = groupUserRepository;
            _workTicketRepository = workTicketRepository;
        }

        // GET: api/GroupUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupUser>>> GetGroupUsers([System.Web.Http.FromUri] int? id = null, [System.Web.Http.FromUri] bool condition = true)
        {
            var list = await _groupUserRepository.GetConditionGroupUsersByGroupId(id, condition);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        //// GET: api/GroupUsers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GroupUser>> GetGroupUser(int id)
        //{
        //    var groupUser = await _context.GroupUsers.FindAsync(id);

        //    if (groupUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return groupUser;
        //}

        // PUT: api/GroupUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupUser(int id, HMS_BE.DTO.GroupUser groupUser)
        {
            if (id != groupUser.Id || groupUser.IsLeader == false)
            {
                return BadRequest();
            }

            try
            {
                await _groupUserRepository.UpdateGroupUser(groupUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _groupUserRepository.GetGroupUserByID(groupUser.Id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/GroupUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupUser>> PostGroupUser(HMS_BE.DTO.GroupUser groupUser)
        {
            try
            {
                await _groupUserRepository.AddGroupUser(groupUser);
            }
            catch (DbUpdateException)
            {
                if (await _groupUserRepository.GetGroupUsersByGroupId(groupUser.Id) != null)
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetGroup", new { id = groupUser.Id }, groupUser);
        }

        //[HttpPost]
        //public async Task<ActionResult<GroupUser>> PostGroupUser([FromBody] List<int> list)
        //{
        //    try
        //    {
        //        foreach(var id in list)
        //        {
        //            var gru = await 
        //        }
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (await _groupUserRepository.GetGroupUsersByGroupId(groupUser.Id) != null)
        //        {
        //            return Conflict();
        //        }

        //        throw;
        //    }

        //    return CreatedAtAction("GetGroup", new { id = groupUser.Id }, groupUser);
        //}

        // DELETE: api/GroupUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupUser(int id)
        {
            var groupUser = await _groupUserRepository.GetGroupUserByID(id);
            if (groupUser == null)
            {
                return NotFound();
            }

            var canLeave = await _workTicketRepository.CanLeaveGroup(id);
            if(canLeave)
            {
                return Conflict(new HMS_BE.DTO.Error { Message = "You must finish all work tickets to leave the group" });
            }

            await _groupUserRepository.RemoveGroupUser(id);
            return NoContent();
        }

        private bool GroupUserExists(int id)
        {
            return _context.GroupUsers.Any(e => e.Id == id);
        }
    }
}
