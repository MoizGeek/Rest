using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoizProject.Model;

namespace MoizProject.Controllers
{
    [Route("api/[controller]")]
    public class BeneficiaryController : Controller
    {

        // GET api/Beneficiary
        [HttpGet]
        public IEnumerable<string> GetAllBeneficiary()
        {
            List<string> returnList = new List<string>();
            foreach (Beneficiary b in Beneficiary.beneficiaries)
            {
                returnList.Add(b.Name);
            }
            return returnList;
        }

        // POST api/Beneficiary
        [HttpPost]
        public IActionResult AddBeneficiary([FromBody]string beneficiaryName)
        {
            // Name must be between 1 and 20 characters
            if (string.IsNullOrEmpty(beneficiaryName) || beneficiaryName.Length > 20)
            {
                return BadRequest("Name must be between 1 and 20 characters.");
            }

            // Maximum added beneficiaries cannot exceed 5
            if (Beneficiary.beneficiaries.Count >= 5)
            {
                return BadRequest("Maximum added beneficiaries cannot exceed 5.");
            }

            // Check if Beneficiary already exists
            if (Beneficiary.beneficiaries.Exists(u => u.Name == beneficiaryName))
            {
                return Conflict("Beneficiary already exists.");
            }

            Beneficiary newBeneficiary = new Beneficiary { Name = beneficiaryName, Balance = 0 };
            Beneficiary.beneficiaries.Add(newBeneficiary);

            return Ok(newBeneficiary.Name);

        }

        private IActionResult Conflict(string message)
        {
            return StatusCode(409, message);
        }
    }
}
