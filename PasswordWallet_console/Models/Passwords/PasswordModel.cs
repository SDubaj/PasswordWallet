using PasswordWallet_console.Entities;
using PasswordWallet_console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordWallet_console.Models.Passwords
{
    public class PasswordModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string LoginPassword { get; set; }
        public string Website { get; set; }
        
        public string Key { get; set; }
        public int Userid { get; set; }
/*
        public User user { get; set; }*/
    }
}
