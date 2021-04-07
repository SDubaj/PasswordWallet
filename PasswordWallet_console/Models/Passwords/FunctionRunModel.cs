using PasswordWallet_console.Entities;
using PasswordWallet_console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordWallet_console.Models.Passwords
{
    public class functionRunModel
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        public int Userid { get; set; }
        public int FunctionId { get; set; }/*
        public IList<FunctionType> FunctionTypes { get; set; }*/
    }
}
