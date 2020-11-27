using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG3050_CVGSClub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace PROG3050_CVGSClub.Controllers
{
    public class GameReviewsController : Controller
    {
        private readonly CvgsClubContext _context;

        public GameReviewsController(CvgsClubContext context)
        {
            _context = context;
        }

        // GET: GameReviews
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string memberId = HttpContext.Session.GetString("userId");
            string url = "/Identity/Account/Login";
            if (memberId == null)
            {
                return LocalRedirect(url);
            }
            else
            {
                var cvgsClubContext = _context.GameReviews.Include(g => g.Game).Include(g => g.Member).Where(m => m.MemberId == memberId);
                return View(await cvgsClubContext.ToListAsync());
            }  
        }

        // GET: GameReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameReviews = await _context.GameReviews
                .Include(g => g.Game)
                .Include(g => g.Member)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (gameReviews == null)
            {
                return NotFound();
            }

            return View(gameReviews);
        }

        // GET: GameReviews/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameName");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return View();
        }

        // POST: GameReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,MemberId,GameId,GameReview")] GameReviews gameReviews)
        {
            if (ModelState.IsValid)
            {
                gameReviews.MemberId = HttpContext.Session.GetString("userId");
                _context.Add(gameReviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameName", gameReviews.GameId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", gameReviews.MemberId);
            return View(gameReviews);
        }

        // GET: GameReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameReviews = await _context.GameReviews.FindAsync(id);
            if (gameReviews == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameName", gameReviews.GameId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", gameReviews.MemberId);
            return View(gameReviews);
        }

        // POST: GameReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,MemberId,GameId,GameReview")] GameReviews gameReviews)
        {
            if (id != gameReviews.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    gameReviews.MemberId = HttpContext.Session.GetString("userId");
                    _context.Update(gameReviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameReviewsExists(gameReviews.ReviewId))
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
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameName", gameReviews.GameId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", gameReviews.MemberId);
            return View(gameReviews);
        }

        // GET: GameReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameReviews = await _context.GameReviews
                .Include(g => g.Game)
                .Include(g => g.Member)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (gameReviews == null)
            {
                return NotFound();
            }

            return View(gameReviews);
        }

        // POST: GameReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameReviews = await _context.GameReviews.FindAsync(id);
            _context.GameReviews.Remove(gameReviews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Check if Item exists
        private bool GameReviewsExists(int id)
        {
            return _context.GameReviews.Any(e => e.ReviewId == id);
        }
    }
}
