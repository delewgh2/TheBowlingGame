using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cashing.Infrastructure;
using Cashing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheBowlingGame.Logic;
using TheBowlingGame.Models;
using TheBowlingGame.Models.ViewModels;

namespace TheBowlingGame.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICacheProvider _cacheProvider;
        private IGame Game;
        private IEnumerable<Frame> frames;

        [BindProperty]
        public ValidateModel ValidateMV { get; set; }
        public HomeController(ILogger<HomeController> logger, ICacheProvider cacheProvider, IGame game)
        {
            _logger = logger;
            _cacheProvider = cacheProvider;
            Game = game;            
        }

        public IActionResult Index()
        {
            var reset = false;

            if (TempData.ContainsKey("Reset"))
                reset = Convert.ToBoolean(TempData["Reset"]);

            if (reset)
                _cacheProvider.ClearCache(CacheKeys.Frames);

            Game = new Game(_cacheProvider);
            frames = Game.Scores();
            return View(frames);
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken()]
        public IActionResult IndexPost(string Roll)
        {
            if (!ModelState.IsValid)
            {
                frames = Game.Scores();
                return View(frames);
            }
                

            Game.Roll(Convert.ToInt32(Roll));
            frames = Game.Scores();
            return View(frames);
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
