using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoizProject.Model;
using System.Net.Http;

namespace MoizProject.Controllers
{
    [Route("api/[controller]")]
    public class TopUpController : Controller
    {
        // GET api/TopUp
        [HttpGet]
        public IEnumerable<decimal> GetAllTopUpOptions()
        {
            List<decimal> returnList = new List<decimal>();
            foreach (TopUp b in TopUp.options)
            {
                returnList.Add(b.Amount);
            }
            return returnList;
        }

        // POST api/TopUp
        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] Transaction transactionModel)
        {
            // Per Month Limits
            decimal maxPerBeneficiaryAmountLimitPerMonth = 500;
            decimal maxAmountLimitPerMonth = 3000;

            // Fetch Target Beneficiary
            Beneficiary currentBeneficiary = Beneficiary.beneficiaries.Find(u => u.Name == transactionModel.BeneficiaryName);

            // Check Beneficiary
            if (currentBeneficiary == null)
            {
                return BadRequest("Invalid Beneficiary.");
            }

            // Increase maxPerBeneficiaryAmountLimit to 1000 if User is not Verified
            if (!TopUpUser.user.Verified)
            {
                maxPerBeneficiaryAmountLimitPerMonth = 1000;
            }

            decimal userBalance = 0;
            string userBalanceString = "0";

            try
            {
                // Fetch User Balance from External API
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(Request.Scheme + "://" + Request.Host.Value + "/api/User");

                if (response.IsSuccessStatusCode)
                {
                    userBalanceString = await response.Content.ReadAsStringAsync();
                }

                userBalance = Convert.ToDecimal(userBalanceString);
            }
            catch (Exception ex)
            {
                return Conflict("Check External HTTP Service Endpoint.");
            }

            // Charge Amount 
            decimal charges = 1;

            // Add Charge Amount to Transaction Amount
            decimal totalTransactionAmount = transactionModel.Amount + charges;

            // Check User Balance
            if (totalTransactionAmount > userBalance)
            {
                return BadRequest("Insufficient Balance.");
            }

            // Check Total Limit of User (Per Month will be applied once Database if available)
            if (TopUpUser.user.TotalAmountCredited + totalTransactionAmount > maxAmountLimitPerMonth)
            {
                return BadRequest("User has reached maximum limit of top-up per Month.");
            }

            // Check Total Limit of User per Beneficiary (Per Month will be applied once Database if available)
            if (currentBeneficiary.TotalAmountCredited + totalTransactionAmount > maxPerBeneficiaryAmountLimitPerMonth)
            {
                return BadRequest("User has reached maximum limit of top-up per Month for this beneficiary.");
            }

            // Debit User Balance
            TopUpUser.user.Balance = TopUpUser.user.Balance - totalTransactionAmount;
            // For Limit
            TopUpUser.user.TotalAmountCredited = TopUpUser.user.TotalAmountCredited + totalTransactionAmount;

            // Credit Beneficiary Balance
            currentBeneficiary.Balance = currentBeneficiary.Balance + totalTransactionAmount;
            // For Limit
            currentBeneficiary.TotalAmountCredited = currentBeneficiary.TotalAmountCredited + totalTransactionAmount;

            return Ok(transactionModel);

        }

        private IActionResult Conflict(string message)
        {
            return StatusCode(409, message);
        }
    }
}