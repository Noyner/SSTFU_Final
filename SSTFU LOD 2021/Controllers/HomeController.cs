using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSTFU_LOD_2021.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bll.Interfaces;
using GarbageDetectionService;

namespace SSTFU_LOD_2021.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       // private readonly EfDao
       private readonly ITechBLO _techBlo;
        public HomeController(ILogger<HomeController> logger,ITechBLO techBlo)
        {
            _logger = logger;
            this._techBlo = techBlo;
        }

        public async Task<IActionResult> Index()
        {
            //---
            await _techBlo.PrefillDB(10);
            //---
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        public async Task<IActionResult> HandleCameraFrames()
        {
            await _techBlo.HandleQueue();
            return RedirectToAction("Index");
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
