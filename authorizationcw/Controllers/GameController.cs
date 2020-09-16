using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using authorizationcw.Models;
using authorizationcw.Data;

namespace authorizationcw.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }
// ---------------------------------------------------------------------------

        public IActionResult ViewGames()
        {
            // return View(_context);
            return Content($"ViewGames");
        }

        public IActionResult ViewDetails()
        {
            return Content($"ViewDetails");
        }
// ---------------------------------------------------------------------------

        [HttpPost]
        public IActionResult AddGame()
        {
            return Content($"AddGame");
        }

        public IActionResult AddGameForm()
        {
            return Content($"ViewDetails");
        }
// ---------------------------------------------------------------------------

        [HttpPost]
        public IActionResult UpdateGame()
        {
            return Content($"UpdateGame");
        }

        public IActionResult UpdateGameForm()
        {
            return Content($"ViewDetails");
        }
// ---------------------------------------------------------------------------

        public IActionResult DeleteGame()
        {
            return Content($"DeleteGame");
        }

        public IActionResult DeleteConfirm()
        {
            return Content($"ViewDetails");
        }

    }
}