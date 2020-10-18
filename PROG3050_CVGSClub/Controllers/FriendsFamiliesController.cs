using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG3050_CVGSClub.Models;

namespace PROG3050_CVGSClub.Controllers
{
    public class FriendsFamiliesController : Controller
    {
        private readonly CvgsClubContext _context;

        public FriendsFamiliesController(CvgsClubContext context)
        {
            _context = context;
        }

        // GET: FriendsFamilies
        public async Task<IActionResult> Index()
        {
            var cVGSClubContext = _context.FriendsFamily.Include(f => f.Member);
            return View(await cVGSClubContext.ToListAsync());
        }

        // GET: FriendsFamilies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendsFamily = await _context.FriendsFamily
                .Include(f => f.Member)
                .FirstOrDefaultAsync(m => m.FriendFamilyId == id);
            if (friendsFamily == null)
            {
                return NotFound();
            }

            return View(friendsFamily);
        }

        // GET: FriendsFamilies/Create
        public async Task<IActionResult> Create()
        {
            var cVGSClubContext = _context.FriendsFamily.Include(f => f.Member);
            //ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "DisplayName");
            return View(await cVGSClubContext.ToListAsync());
        }

        // POST: FriendsFamilies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FriendFamilyId,MemberId,FriendId")] FriendsFamily friendsFamily)
        {
            if (ModelState.IsValid)
            {
                _context.Add(friendsFamily);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "DisplayName", friendsFamily.MemberId);
            return View(friendsFamily);
        }

        // GET: FriendsFamilies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendsFamily = await _context.FriendsFamily.FindAsync(id);
            if (friendsFamily == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "DisplayName", friendsFamily.MemberId);
            return View(friendsFamily);
        }

        // POST: FriendsFamilies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FriendFamilyId,MemberId,FriendId")] FriendsFamily friendsFamily)
        {
            if (id != friendsFamily.FriendFamilyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friendsFamily);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendsFamilyExists(friendsFamily.FriendFamilyId))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "DisplayName", friendsFamily.MemberId);
            return View(friendsFamily);
        }

        // GET: FriendsFamilies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendsFamily = await _context.FriendsFamily
                .Include(f => f.Member)
                .FirstOrDefaultAsync(m => m.FriendFamilyId == id);
            if (friendsFamily == null)
            {
                return NotFound();
            }

            return View(friendsFamily);
        }

        // POST: FriendsFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friendsFamily = await _context.FriendsFamily.FindAsync(id);
            _context.FriendsFamily.Remove(friendsFamily);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendsFamilyExists(int id)
        {
            return _context.FriendsFamily.Any(e => e.FriendFamilyId == id);
        }
    }
}
