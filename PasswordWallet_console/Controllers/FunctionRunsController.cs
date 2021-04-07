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
    public class functionRunsController : ControllerBase
    {
        private readonly DataContext _context;

        public functionRunsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/functionRuns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<functionRun>>> GetfunctionRun()
        {
            return await _context.functionRun.ToListAsync();
        }

        // GET: api/functionRuns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<functionRun>> GetfunctionRun(int id)
        {
            var functionRun = await _context.functionRun.FindAsync(id);

            if (functionRun == null)
            {
                return NotFound();
            }

            return functionRun;
        }

        // PUT: api/functionRuns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutfunctionRun(int id, functionRun functionRun)
        {
            if (id != functionRun.Id)
            {
                return BadRequest();
            }

            _context.Entry(functionRun).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!functionRunExists(id))
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

        // POST: api/functionRuns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<functionRun>> PostfunctionRun(functionRun functionRun)
        {
            _context.functionRun.Add(functionRun);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetfunctionRun", new { id = functionRun.Id }, functionRun);
        }

        // DELETE: api/functionRuns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<functionRun>> DeletefunctionRun(int id)
        {
            var functionRun = await _context.functionRun.FindAsync(id);
            if (functionRun == null)
            {
                return NotFound();
            }

            _context.functionRun.Remove(functionRun);
            await _context.SaveChangesAsync();

            return functionRun;
        }

        private bool functionRunExists(int id)
        {
            return _context.functionRun.Any(e => e.Id == id);
        }
    }
}
