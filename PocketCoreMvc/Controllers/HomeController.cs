using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PocketCoreMvc.Models;
using PocketCoreMvc.Services;

namespace PocketCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiService apiService;
        public HomeController(IApiService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
           
            return View();
        }

        public async Task<IActionResult> About()
        {
            var values = await apiService.AuthenticateRequest();
            if (values != null) {
              return Redirect(values);
            }

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
