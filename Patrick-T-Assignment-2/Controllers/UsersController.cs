using Microsoft.AspNetCore.Mvc;

namespace Patrick_T_Assignment_2.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
