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
    public interface IPasswordService
    {
        Password Encrypt(Password password, int playerID);
        Password Decrypt(int playerID, string login);
        void Update(Password password, string newPassword = null);
        IEnumerable<Password> GetAll();
        void Delete(int id);
    }
    public class PasswordService : IPasswordService
    {
        private DataContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        private int myPlayerId;

        public PasswordService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
             _httpContextAccessor = httpContextAccessor;
            _context = context;
            myPlayerId = int.Parse(httpContextAccessor.HttpContext.User.Identity.Name);
        }


       

        public Password Encrypt(Password password, int playerID)
        {
            //register creating password
            var functionRun = new functionRun();
            functionRun.Userid = playerID;
            functionRun.Timestamp = DateTime.Now.Date;
            var temp = _context.Functions.Where(i => i.Function_name == "ADD_PASSWORD").FirstOrDefault();
            functionRun.FunctionId = temp.Id;
            _context.functionRun.Add(functionRun);


            //check if user already exists
            if (_context.Passwords.Any(x => x.Login == password.Login))
                throw new AppException("Login \"" + password.Login + "\" is already taken");

            //encrypt password
            string encryptedPassword;
            EncryptPassword(password.Key, password.LoginPassword, out encryptedPassword);

            //overwrite object and add to database
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

            DecryptPass(word);
            return word;
        }
        public void Delete(int id)
        {
            //register deleting password
            var functionRun = new functionRun();
            functionRun.Userid = myPlayerId;
            functionRun.Timestamp = DateTime.Now.Date;
            var temp = _context.Functions.Where(i => i.Function_name == "DELETE_PASSWORD").FirstOrDefault();
            functionRun.FunctionId = temp.Id;
            _context.functionRun.Add(functionRun);

            //check if password exsist and delete
            var password = _context.Passwords.Find(id);
            if (password != null)
            {
                _context.Passwords.Remove(password);
                _context.SaveChanges();
            }

        }

        public void Update(Password passwordParam, string newPassword = null)
        {
            //create object with DataChangeModel
            var dataChangeObject = new DataChange();
            dataChangeObject.PreviousValue = passwordParam.LoginPassword;
            dataChangeObject.Userid = myPlayerId;
            dataChangeObject.Date = DateTime.Now;
            var ActionTypeId= _context.ActionTypes.Where(i => i.Title == "PASSWORD_UPDATE").FirstOrDefault();
            dataChangeObject.ActionTypeId = ActionTypeId.Id;
            dataChangeObject.ModifiedRecord = passwordParam.Id;


            //get password's details
            var password = _context.Passwords.Find(passwordParam.Id);

            //register updating password
            var functionRun = new functionRun();
            functionRun.Userid = myPlayerId;
            functionRun.Timestamp = DateTime.Now.Date;
            var temp = _context.Functions.Where(i => i.Function_name == "UPDATE_PASSWORD").FirstOrDefault();
            functionRun.FunctionId = temp.Id;
            _context.functionRun.Add(functionRun);
            //////////////////


            //check if password exists
            if (password == null)
                throw new AppException("User not found");

            if (_context.Passwords.Any(x => x.Login == passwordParam.Login))
                throw new AppException("Login \"" + password.Login + "\" is already taken");

            //encrypt new password
            string encryptedPassword;
            EncryptPassword(password.Key, newPassword, out encryptedPassword);

            //overwrite data
            dataChangeObject.PresentValue = encryptedPassword;
            password.Login = passwordParam.Login;
            password.LoginPassword = encryptedPassword;

            //update database
            _context.DataChanges.Add(dataChangeObject);
            _context.Passwords.Update(password);
            _context.SaveChanges();
        }



        public void EncryptPassword(string key, string plainText, out string encryptedPassword)
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

        public void DecryptPass(Password word)
        {

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
        }

       
    }
}
