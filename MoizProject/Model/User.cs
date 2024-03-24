using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoizProject.Model
{
    public class TopUpUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public bool Verified { get; set; }
        public decimal TotalAmountCredited { get; set; }

        // Here you can set the starting balance and status of the user
        public static TopUpUser user = new TopUpUser
        { Id = 1, Name = "Moiz", Balance = 10000, Verified = true, TotalAmountCredited = 0 };
    }
}
