﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BedeThirteen.App.Models;

namespace BedeThirteen.App.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60 * 60)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact support.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
