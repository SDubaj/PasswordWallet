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
          

        }
    }
}

    

