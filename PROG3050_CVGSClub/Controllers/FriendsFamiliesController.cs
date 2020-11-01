using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        // This just shows all the friends and family
        // No filter for the member ID yet
        public async Task<IActionResult> Index()
        {
            string memberId = HttpContext.Session.GetString("userId");
            var cVGSClubContext = _context.FriendsFamily.Include(f => f.Member).Where(f => f.MemberId == memberId);
            return View(await cVGSClubContext.ToListAsync());
        }

        // GET: FriendsFamilies/Details/5
        // Shows the selected family and friend details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var friendsFamily = await _context.FriendsFamily
                .Include(f => f.Member)
                .FirstOrDefaultAsync(m => m.FriendFamilyId == id);

            if (friendsFamily == null)
                return NotFound();

            return View(friendsFamily);
        }

        // GET: FriendsFamilies/Create
        // Optional: Instead of filtering out all the already added members,
        // figure out how to leave them on the list, but disable the add button for them
        public async Task<IActionResult> Create()
        {
            string memberId = HttpContext.Session.GetString("userId");
            var friendContext = _context.FriendsFamily.Include(f => f.Member).Where(f => f.MemberId == memberId);
            var cVGSClubContext = _context.Members.Where(a => friendContext.All(f => f.Member.MemberId != a.MemberId));
           
            return View(await cVGSClubContext.ToListAsync());
        }

        // [ValidateAntiForgeryToken]
        // To fix the MemberID and Member problem, the Member CvgsClubContext had to change its foreign key connection
        // Member now has a foreign connection to the friendID rather than the memberID
        public async Task<IActionResult> Add(string id, FriendsFamily friendsFamily)
        {
            string memberId = HttpContext.Session.GetString("userId");
            friendsFamily.MemberId = memberId;
            friendsFamily.FriendId = id;
            _context.Add(friendsFamily);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: FriendsFamilies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var friendsFamily = await _context.FriendsFamily
                .Include(f => f.Member)
                .FirstOrDefaultAsync(m => m.FriendFamilyId == id);

            if (friendsFamily == null)
                return NotFound();

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
