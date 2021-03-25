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
    public interface IPasswordService
    {
        Password Encrypt(Password password, int playerID);
        Password Decrypt(int playerID, string login);
        IEnumerable<Password> GetAll();
    }
    public class PasswordService : IPasswordService
    {
        private DataContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public PasswordService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
             _httpContextAccessor = httpContextAccessor;
            _context = context;
        }


       

        public Password Encrypt(Password password, int playerID)
        {

            if (_context.Passwords.Any(x => x.Login == password.Login))
                throw new AppException("Login \"" + password.Login + "\" is already taken");
            string encryptedPassword;

            EncryptPassword(password.Key, password.LoginPassword, out encryptedPassword);

            password.LoginPassword = encryptedPassword;
            password.UserId = playerID;
            _context.Passwords.Add(password);

            _context.SaveChanges();

            return password;
        }

        //get all passwords
        public IEnumerable<Password> GetAll()
        {
            return _context.Passwords;
        }


        public Password Decrypt(int playerID,string login)
        {
            /*var word =  _context.Passwords.FindAsync(id);*/
            var words =  _context.Passwords.Where(p => p.UserId == playerID);
            /*          var hasla = _context.Passwords;*/

            var word = words.SingleOrDefault(x => x.Login == login);

            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(word.LoginPassword);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(word.Key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            word.LoginPassword = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return word;
        }




        private static void EncryptPassword(string key, string plainText, out string encryptedPassword)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            encryptedPassword = Convert.ToBase64String(array);
        }

        
    }
}
