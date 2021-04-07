using PasswordWallet_console.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordWallet_console.Entities
{
    public class Password
    {
        /*internal string password;*/

        public int Id { get; set; }
        public string Login { get; set; }
        public string LoginPassword { get; set; }
        /*public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }*/
        public string Website { get; set; }
        public string Key { get; set; }

        public int UserId { get; set; }
        /*public User user { get; set; }*/
    }
}
