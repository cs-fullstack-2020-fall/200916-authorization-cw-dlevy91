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
            return View(_context);
            // return Content($"ViewGames");
        }

        public IActionResult ViewDetails(int gameID)
        {
            GameModel foundGame = _context.games.FirstOrDefault(g => g.id == gameID);
            if(foundGame != null)
            {
                return View(foundGame);
            }
            return Content($"No game with ID of {gameID}");
        }
// ---------------------------------------------------------------------------

        [HttpPost]
        public IActionResult AddGame(GameModel newGame)
        {
            if(ModelState.IsValid)
            {
                _context.games.Add(newGame);
                _context.SaveChanges();
                return RedirectToAction("ViewGames");
            }
            else{
                return View("AddGameForm", newGame);
            }
        }

        public IActionResult AddGameForm()
        {
            return View();
        }
// ---------------------------------------------------------------------------

        [HttpPost]
        public IActionResult UpdateGame(GameModel updateGame)
        {
            GameModel foundGame = _context.games.FirstOrDefault(g => g.id == updateGame.id);
            if(foundGame != null)
            {
                if(ModelState.IsValid)
                {
                    foundGame.Title = updateGame.Title;
                    foundGame.Publisher = updateGame.Publisher;
                    foundGame.Rating = updateGame.Rating;
                    foundGame.Description = updateGame.Description;
                    _context.SaveChanges();
                    return RedirectToAction("ViewGames");
                }
                else{
                    return View("UpdateGameForm", updateGame);
                }
            }
            else{
                return Content($"No Game found with the ID of {updateGame.id}");
            }
            
        }

        public IActionResult UpdateGameForm(int gameID)
        {
            GameModel foundGame = _context.games.FirstOrDefault(g => g.id == gameID);
            if(foundGame != null)
            {
                return View(foundGame);
            }
            return Content($"No Game found with the ID of {gameID}");
        }
// ---------------------------------------------------------------------------

        public IActionResult DeleteGame(int gameID)
        {
            GameModel foundGame = _context.games.FirstOrDefault(g => g.id == gameID);
            if(foundGame != null)
            {
                _context.Remove(foundGame);
                _context.SaveChanges();
                return RedirectToAction("ViewGames");
            }
            else{
                return Content($"No Game found with the ID of {gameID} to delete");
            }
        }

        public IActionResult DeleteConfirm(int gameID)
        {
            GameModel foundGame = _context.games.FirstOrDefault(g => g.id == gameID);
            if(foundGame != null)
            {
                return View(foundGame);
            }
            else{
                return Content($"No Game found with the ID of {gameID} to delete");
            }
        }

    }
}