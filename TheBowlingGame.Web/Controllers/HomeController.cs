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
using TheBowlingGame.Web.Helper;

namespace TheBowlingGame.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICacheProvider _cacheProvider;
        private IGame Game;
        private IEnumerable<Frame> frames;

        [BindProperty]
        public PinViewModel PinVM { get; set; }
        public HomeController(ILogger<HomeController> logger, ICacheProvider cacheProvider, IGame game)
        {
            _logger = logger;
            _cacheProvider = cacheProvider;
            Game = game;            
        }

        public IActionResult Index()
        {
            var reset = false;
            TempData[Keys.IsVisible] = true;

            if (TempData.ContainsKey(Keys.Reset))
                reset = Convert.ToBoolean(TempData[Keys.Reset]);

            if (reset)
                _cacheProvider.ClearCache(Keys.Frames);

            Game = new Game(_cacheProvider);
            frames = Game.Scores();
            return View(FrameViewBuilder.FrameView(frames));
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken()]
        public IActionResult IndexPost(string Pin)
        {
            if (ModelState.IsValid)
                Game.Roll(Convert.ToInt32(Pin));
            
            frames = Game.Scores();
            var framesView = FrameViewBuilder.FrameView(frames);
            var frame = framesView.Find(f => f.IsReset == true && f.FrameId == 10);

            if (frame != null && frame.IsReset)
                TempData[Keys.IsVisible] = false;

            return View(framesView);
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
