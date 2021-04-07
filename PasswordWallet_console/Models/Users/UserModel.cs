/*using PasswordWallet_console.Entities;*/
using PasswordWallet_console.Models;
using PasswordWallet_console.Models.DataChange;
using PasswordWallet_console.Models.Passwords;
using System.Collections.Generic;

namespace PasswordWallet_console.Models.Users

{
  public class UserModel
    {
        public int Id { get; set; }
        /*public string FirstName { get; set; }
        public string LastName { get; set; }*/
        public string Login { get; set; }
       /* public string Role { get; set; }*/
        public  ICollection<PasswordModel> Passwords { get; set; }

        public ICollection<functionRunModel> Functions { get; set; }
        public ICollection<DataChangeModel> DataChanges { get; set; }
        /*public Password Passwords { get; set; }*/
        /*public Password Passwords { get; set; }*/
    }
}