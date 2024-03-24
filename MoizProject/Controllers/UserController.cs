
using Microsoft.AspNetCore.Mvc;
using MoizProject.Model;

namespace MoizProject.Controllers
{
    [Route("api/[controller]")]

    public class UserController : Controller
    {
        // GET api/User
        [HttpGet]
        public decimal GetUserBalance()
        {
            return TopUpUser.user.Balance;
        }
    }
}