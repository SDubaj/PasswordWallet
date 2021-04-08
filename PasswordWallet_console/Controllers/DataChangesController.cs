using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordWallet_console.Entities;
using PasswordWallet_console.Helpers;

namespace PasswordWallet_console.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataChangesController : ControllerBase
    {
        private readonly DataContext _context;

        public DataChangesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DataChanges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataChange>>> GetDataChanges()
        {
            return await _context.DataChanges.Include(a => a.ActionTypes).ToListAsync();
        }

        // GET: api/DataChanges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataChange>> GetDataChange(int id)
        {
            var dataChange = await _context.DataChanges.Include(a => a.ActionTypes).FirstOrDefaultAsync(i => i.Id == id); ;

            if (dataChange == null)
            {
                return NotFound();
            }

            return dataChange;
        }

        // PUT: api/DataChanges/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataChange(int id, DataChange dataChange)
        {
            if (id != dataChange.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataChange).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataChangeExists(id))
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

        // POST: api/DataChanges
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataChange>> PostDataChange(DataChange dataChange)
        {
            _context.DataChanges.Add(dataChange);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataChange", new { id = dataChange.Id }, dataChange);
        }

        // DELETE: api/DataChanges/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataChange>> DeleteDataChange(int id)
        {
            var dataChange = await _context.DataChanges.FindAsync(id);
            if (dataChange == null)
            {
                return NotFound();
            }

            _context.DataChanges.Remove(dataChange);
            await _context.SaveChangesAsync();

            return dataChange;
        }

        private bool DataChangeExists(int id)
        {
            return _context.DataChanges.Any(e => e.Id == id);
        }
    }
}
