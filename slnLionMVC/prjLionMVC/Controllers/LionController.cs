using Microsoft.AspNetCore.Mvc;

namespace prjLionMVC.Controllers
{
    public class LionController : Controller
    {
        public IActionResult MsgList()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}