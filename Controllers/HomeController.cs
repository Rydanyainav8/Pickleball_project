using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Pickleball_project.Models;
using Pickleball_project.Repository;
using System.Data;
using System.Diagnostics;

namespace Pickleball_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IWebHostEnvironment _webHostEnvironment;
        private readonly IClientRepository _clientRepository;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IClientRepository clientRepository)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _clientRepository = clientRepository; 
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormFile formFile)
        {
            string path = _clientRepository.DocumentUpload(formFile);
            DataTable dt = _clientRepository.ClientDataTable(path);
            _clientRepository.ImportClient(dt);
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}