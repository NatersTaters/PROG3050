using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG3050_CVGSClub.Models;

namespace PROG3050_CVGSClub.Controllers
{
    public class GameLibrariesController : Controller
    {
        private readonly CvgsClubContext _context;

        public GameLibrariesController(CvgsClubContext context)
        {
            _context = context;
        }

        // GET: GameLibraries
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cvgsClubContext = _context.GamesLibrary.Include(g => g.Game).Include(g => g.Member);
            return View(await cvgsClubContext.ToListAsync());
        }

        // GET: GameLibraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamesLibrary = await _context.GamesLibrary
                .Include(g => g.Game)
                .Include(g => g.Member)
                .FirstOrDefaultAsync(m => m.LibraryGameId == id);
            if (gamesLibrary == null)
            {
                return NotFound();
            }

            return View(gamesLibrary);
        }

        // GET: GameLibraries/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "AvailablePlatforms");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return View();
        }

        // POST: GameLibraries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibraryGameId,MemberId,GameId")] GamesLibrary gamesLibrary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamesLibrary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "AvailablePlatforms", gamesLibrary.GameId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", gamesLibrary.MemberId);
            return View(gamesLibrary);
        }

        // GET: GameLibraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamesLibrary = await _context.GamesLibrary.FindAsync(id);
            if (gamesLibrary == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "AvailablePlatforms", gamesLibrary.GameId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", gamesLibrary.MemberId);
            return View(gamesLibrary);
        }

        // POST: GameLibraries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibraryGameId,MemberId,GameId")] GamesLibrary gamesLibrary)
        {
            if (id != gamesLibrary.LibraryGameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamesLibrary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamesLibraryExists(gamesLibrary.LibraryGameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "AvailablePlatforms", gamesLibrary.GameId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", gamesLibrary.MemberId);
            return View(gamesLibrary);
        }

        // GET: GameLibraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamesLibrary = await _context.GamesLibrary
                .Include(g => g.Game)
                .Include(g => g.Member)
                .FirstOrDefaultAsync(m => m.LibraryGameId == id);
            if (gamesLibrary == null)
            {
                return NotFound();
            }

            return View(gamesLibrary);
        }

        // POST: GameLibraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gamesLibrary = await _context.GamesLibrary.FindAsync(id);
            _context.GamesLibrary.Remove(gamesLibrary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamesLibraryExists(int id)
        {
            return _context.GamesLibrary.Any(e => e.LibraryGameId == id);
        }
    }
}
