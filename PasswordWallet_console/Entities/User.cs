using PasswordWallet_console.Entities;
using System;
using System.Collections.Generic;

namespace PasswordWallet_console.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        /*public IList<Password> Passwords { get; set; }*/
        public ICollection<Password> Passwords { get; set; }
        public ICollection<functionRun> Functions { get; set; }
        public ICollection<DataChange> DataChanges { get; set; }


    }
}