using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordWallet_console.Models.DataChange
{
    public class DataChangeModel
    {
        public int Id { get; set; }
        public int ModifiedRecord { get; set; }
        public string PreviousValue { get; set; }
        public string PresentValue { get; set; }
        public DateTime Date { get; set; }
        public int Userid { get; set; }
        public int ActionTypeId { get; set; }
    }
}
