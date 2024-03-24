using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoizProject.Model
{
    public class Beneficiary
    {
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalAmountCredited { get; set; }


        public static List<Beneficiary> beneficiaries = new List<Beneficiary>
            {
                new Beneficiary { Name = "John Doe", Balance = 0, TotalAmountCredited = 0},
                new Beneficiary { Name = "Jane Smith", Balance = 0, TotalAmountCredited = 0}
            };
    }

}
