using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoizProject.Model
{
    public class TopUp
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public static List<TopUp> options = new List<TopUp>
            {
                new TopUp { Amount = 5, Currency = "AED"},
                new TopUp { Amount = 10, Currency = "AED"},
                new TopUp { Amount = 20, Currency = "AED"},
                new TopUp { Amount = 30, Currency = "AED"},
                new TopUp { Amount = 50, Currency = "AED"},
                new TopUp { Amount = 75, Currency = "AED"},
                new TopUp { Amount = 100, Currency = "AED"},
            };
    }
}
