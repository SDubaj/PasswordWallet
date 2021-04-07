using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PasswordWallet_console.Entities;
using PasswordWallet_console.Helpers;
using PasswordWallet_console.Models.Passwords;

namespace PasswordWallet_console.Services
{
    /*public interface IUserService
    {
        User Authenticate(string Login, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }*/
    public interface IFunctionService
    {

        IEnumerable<functionRun> GetAll();
    }
    public class FunctionService : IFunctionService
    {
        private DataContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public FunctionService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        //get all passwords
        public IEnumerable<functionRun> GetAll()
        {
            return _context.functionRun;
        }







    }
}
