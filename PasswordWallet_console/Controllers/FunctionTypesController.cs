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
    public class FunctionTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public FunctionTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/FunctionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FunctionType>>> GetFunctions()
        {
            return await _context.Functions.ToListAsync();
        }

        // GET: api/FunctionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FunctionType>> GetFunctionType(int id)
        {
            var functionType = await _context.Functions.FindAsync(id);

            if (functionType == null)
            {
                return NotFound();
            }

            return functionType;
        }

        // PUT: api/FunctionTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFunctionType(int id, FunctionType functionType)
        {
            if (id != functionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(functionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunctionTypeExists(id))
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

        // POST: api/FunctionTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FunctionType>> PostFunctionType(FunctionType functionType)
        {
            _context.Functions.Add(functionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFunctionType", new { id = functionType.Id }, functionType);
        }

        // DELETE: api/FunctionTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FunctionType>> DeleteFunctionType(int id)
        {
            var functionType = await _context.Functions.FindAsync(id);
            if (functionType == null)
            {
                return NotFound();
            }

            _context.Functions.Remove(functionType);
            await _context.SaveChangesAsync();

            return functionType;
        }

        private bool FunctionTypeExists(int id)
        {
            return _context.Functions.Any(e => e.Id == id);
        }
    }
}
