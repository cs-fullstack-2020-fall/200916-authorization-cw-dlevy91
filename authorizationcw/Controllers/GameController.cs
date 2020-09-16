using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using authorizationcw.Models;
using authorizationcw.Data;
using Microsoft.AspNetCore.Authorization;

namespace authorizationcw.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }
// -----Methods responsible for showing data entered to the database----------------------------------------------------------------------
       
        //Authorize will allow only those with the roles stated to access the page specified
        [Authorize(Roles = "user,employee,admin")]
        public IActionResult ViewGames()
        {
            //pulls the DB into the page so it can run through it
            return View(_context);
            // return Content($"ViewGames");
        }

        //Authorize once again with the same people
        [Authorize(Roles = "user,employee,admin")]
        public IActionResult ViewDetails(int gameID)
        {
            //compares gameID above to anything in the list and if found will display just that entry or tell you that no game exists with that id
            GameModel foundGame = _context.games.FirstOrDefault(g => g.id == gameID);
            if(foundGame != null)
            {
                return View(foundGame);
            }
            return Content($"No game with ID of {gameID}");
        }
// ---------------------------------------------------------------------------
        //Now only two people are authorized to access these items
        [Authorize(Roles = "employee,admin")]
        [HttpPost]
        public IActionResult AddGame(GameModel newGame)
        {
            if(ModelState.IsValid)
            {
                //if entered information is accurate it will post it to the db and save the changes then return to the viewgames view
                _context.games.Add(newGame);
                _context.SaveChanges();
                return RedirectToAction("ViewGames");
            }
            else{
                //if entered information is incorrect it will send the page again with the information previously entered on it
                return View("AddGameForm", newGame);
            }
        }

        //displays the forms that will enter the information through the method above to the DB
        [Authorize(Roles = "employee,admin")]
        public IActionResult AddGameForm()
        {
            return View();
        }
// ---------------------------------------------------------------------------

        [Authorize(Roles = "employee,admin")]
        [HttpPost]
        public IActionResult UpdateGame(GameModel updateGame)
        {
            //searches for an entry in the db that has a matching ID and if found will allow you to change the information to whatever you enter
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
                    //if entered information is incorrect it sends the page again with the previous information prepopulated
                    return View("UpdateGameForm", updateGame);
                }
            }
            else{
                //if there is no entry it tells you no entry with that id
                return Content($"No Game found with the ID of {updateGame.id}");
            }
            
        }

        //method that displays the form for the update page, will check against an id passed in and if correct it will allow you to the page, if not it will tell you no game exists with that id
        [Authorize(Roles = "employee,admin")]
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
        //only admin is allowed to delete an entry no other users can access it
        [Authorize(Roles = "admin")]
        //runs through db looking for entry with same id as what was passed in and if found will remove entry from db
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
                //if not found it will tell you no game was found with that id
                return Content($"No Game found with the ID of {gameID} to delete");
            }
        }

        //method to display confirmation page before the entry is removed by the method above
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirm(int gameID)
        {
            //checks against passed in id to what is in the db
            GameModel foundGame = _context.games.FirstOrDefault(g => g.id == gameID);
            if(foundGame != null)
            {
                return View(foundGame);
            }
            else{
                //if nothing found then displays saying no game found with ID
                return Content($"No Game found with the ID of {gameID} to delete");
            }
        }

    }
}