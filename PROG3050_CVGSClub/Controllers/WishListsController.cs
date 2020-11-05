using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG3050_CVGSClub.Models;

namespace PROG3050_CVGSClub.Controllers
{
    public class WishListsController : Controller
    {
        private readonly CvgsClubContext _context;

        public WishListsController(CvgsClubContext context)
        {
            _context = context;
        }

        // GET: WishLists
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cVGSClubContext = _context.WishLists.Include(w => w.Game).Include(w => w.Member);
            return View(await cVGSClubContext.ToListAsync());
        }

        // GET: WishLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishLists = await _context.WishLists
                .Include(w => w.Game)
                .Include(w => w.Member)
                .FirstOrDefaultAsync(m => m.WishId == id);
            if (wishLists == null)
            {
                return NotFound();
            }

            return View(wishLists);
        }

        // GET: WishLists/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "AvailablePlatforms");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return View();
        }

        // POST: WishLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WishId,MemberId,GameId")] WishLists wishLists)
        {
            if (ModelState.IsValid)
            {
                wishLists.MemberId = HttpContext.Session.GetString("userId");
                _context.Add(wishLists);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "AvailablePlatforms", wishLists.GameId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", wishLists.MemberId);
            return View(wishLists);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWishlist(int id)
        {
            var wishList = await _context.WishLists.FindAsync(id);
            if (wishList == null)
            {
                return NotFound();
            }
            string memberId = HttpContext.Session.GetString("userId");

            WishLists wishlist = new WishLists();
            wishlist.MemberId = memberId;
            wishlist.GameId = id;

            _context.Add(wishlist);
            await _context.SaveChangesAsync();

            return View(wishlist);
        }
        // GET: WishLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishLists = await _context.WishLists.FindAsync(id);
            if (wishLists == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "AvailablePlatforms", wishLists.GameId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", wishLists.MemberId);
            return View(wishLists);
        }

        // POST: WishLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WishId,MemberId,GameId")] WishLists wishLists)
        {
            if (id != wishLists.WishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishLists);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishListsExists(wishLists.WishId))
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
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "AvailablePlatforms", wishLists.GameId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", wishLists.MemberId);
            return View(wishLists);
        }

        // GET: WishLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishLists = await _context.WishLists
                .Include(w => w.Game)
                .Include(w => w.Member)
                .FirstOrDefaultAsync(m => m.WishId == id);
            if (wishLists == null)
            {
                return NotFound();
            }

            return View(wishLists);
        }

        // POST: WishLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wishLists = await _context.WishLists.FindAsync(id);
            _context.WishLists.Remove(wishLists);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishListsExists(int id)
        {
            return _context.WishLists.Any(e => e.WishId == id);
        }
    }
}
