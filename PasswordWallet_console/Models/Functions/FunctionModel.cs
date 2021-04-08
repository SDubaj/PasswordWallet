using PasswordWallet_console.Entities;
using PasswordWallet_console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordWallet_console.Models.Functions
{
    public class FunctionModel
    {
        public int Id { get; set; }
       
        public string Function_name { get; set; }

        public string Description { get; set; }
    }
}
