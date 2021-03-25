using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PasswordWallet_console.Entities;
using PasswordWallet_console.Helpers;
using PasswordWallet_console.Models.Passwords;
using PasswordWallet_console.Services;

namespace PasswordWallet_console.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private IPasswordService _passwordService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private int myPlayerId;

        public PasswordsController(
            IPasswordService passwordService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor
            )

        {
            _passwordService = passwordService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            myPlayerId = int.Parse(httpContextAccessor.HttpContext.User.Identity.Name);
            /*int.Parse(httpContextAccessor.HttpContext.User.Identity.Name);*/

        }


        [HttpPost("addPassword")]
        public IActionResult addPassword([FromBody]PasswordModel model)
        {
            // map model to entity
            var password = _mapper.Map<Password>(model);

            try
            {
                // create password
                _passwordService.Encrypt(password, myPlayerId );
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _passwordService.GetAll();
            var model = _mapper.Map<IList<PasswordModel>>(users);
            return Ok(model);
        }

        [HttpGet("getPasswords/{login}")]
        public IActionResult getPassword(string login)
        {
            var user = _passwordService.Decrypt(myPlayerId,  login);
            var model = _mapper.Map<PasswordModel>(user);
            return Ok(model);
            /*var users = _passwordService.Decrypt(myPlayerId, id);
            var model = _mapper.Map<IList<PasswordModel>>(users);
            return Ok(model);*/
            // map model to entity
            /*try
            {
                // create password
                _passwordService.Encrypt(password, model.LoginPassword, model.Key, myPlayerId);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }*/



        }
    }
}

        // GET: api/Passwords
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Password>>> GetPasswords()
        {
            return await _context.Passwords.ToListAsync();
        }*/

        // GET: api/Passwords/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Password>> GetPassword(int id)
        {
            var password = await _context.Passwords.FindAsync(id);

            if (password == null)
            {
                return NotFound();
            }

            return password;
        }

        // PUT: api/Passwords/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassword(int id, Password password)
        {
            if (id != password.Id)
            {
                return BadRequest();
            }

            _context.Entry(password).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasswordExists(id))
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

        // POST: api/Passwords
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Password>> PostPassword(Password password)
        {
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassword", new { id = password.Id }, password);
        }

        // DELETE: api/Passwords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Password>> DeletePassword(int id)
        {
            var password = await _context.Passwords.FindAsync(id);
            if (password == null)
            {
                return NotFound();
            }

            _context.Passwords.Remove(password);
            await _context.SaveChangesAsync();

            return password;
        }

        private bool PasswordExists(int id)
        {
            return _context.Passwords.Any(e => e.Id == id);
        }
    }
    
    }
     */

