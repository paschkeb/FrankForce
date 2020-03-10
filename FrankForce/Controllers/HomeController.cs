using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrankForce.Models;
using DataLibrary.BusinessLogic;

namespace FrankForce.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserProcessor _userProcessor;
        private readonly IOrganizationProcessor _organizationProcessor;

        public HomeController(ILogger<HomeController> logger, IUserProcessor userProcessor, IOrganizationProcessor organizationProcessor)
        {
            _logger = logger;
            _userProcessor = userProcessor;
            _organizationProcessor = organizationProcessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info()
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
