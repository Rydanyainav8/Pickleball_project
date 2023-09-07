using Microsoft.AspNetCore.Mvc;

namespace Pickleball_project.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
