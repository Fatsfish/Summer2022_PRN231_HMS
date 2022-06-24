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
    public class WorksController : ControllerBase
    {
        private readonly IWorkRepository workRepository;

        public WorksController(IMapper mapper)
        {
            workRepository = new WorkRepository(mapper);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Work>>> GetWorks([FromQuery]bool isAvailable)
        { 
            if (isAvailable)
            {
                var availableList = await workRepository.GetAvalableWorkList();
                return Ok(availableList);
            }
            var list = await workRepository.GetWorkList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Work>> GetWork(int id)
        {
            var work = workRepository.GetWorkById(id);

            if (work == null)
            {
                return NotFound();
            }

            return Ok(work);
        }

        // PUT: api/Works/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWork(int id, Work work)
        {
            if (id != work.Id)
            {
                return BadRequest();
            }

            try
            {
                await workRepository.UpdateWork(work);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return NoContent();
        }

        // POST: api/Works
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Work>> PostWork(Work work)
        {
            await workRepository.AddWork(work);

            return CreatedAtAction("GetWork", new { id = work.Id }, work);
        }

        // DELETE: api/Works/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork(int id)
        {
            await workRepository.DeleteWork(id);

            return NoContent();
        }
    }
}
