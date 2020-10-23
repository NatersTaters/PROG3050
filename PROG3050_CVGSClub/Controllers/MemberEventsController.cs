using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG3050_CVGSClub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace PROG3050_CVGSClub.Controllers
{
    public class MemberEventsController : Controller
    {
        private readonly CvgsClubContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MemberEventsController(CvgsClubContext context)
        {
            _context = context;
        }

        // GET: MemberEvents
        public async Task<IActionResult> Index()
        {
            string memberId = HttpContext.Session.GetString("userId");
            var cvgsClubContext = _context.MemberEvents.Include(m => m.Event).Include(m => m.Member).Where(m => m.MemberId == memberId);
            return View(await cvgsClubContext.ToListAsync());
        }

        // GET: MemberEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberEvents = await _context.MemberEvents
                .Include(m => m.Event)
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.MemberEventsId == id);
            if (memberEvents == null)
            {
                return NotFound();
            }

            return View(memberEvents);
        }

        // GET: MemberEvents/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return View();
        }

        // POST: MemberEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberEventsId,EventId,MemberId")] MemberEvents memberEvents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberEvents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", memberEvents.EventId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", memberEvents.MemberId);
            return View(memberEvents);
        }

        // GET: MemberEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberEvents = await _context.MemberEvents.FindAsync(id);
            if (memberEvents == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", memberEvents.EventId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", memberEvents.MemberId);
            return View(memberEvents);
        }

        // POST: MemberEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberEventsId,EventId,MemberId")] MemberEvents memberEvents)
        {
            if (id != memberEvents.MemberEventsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberEvents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberEventsExists(memberEvents.MemberEventsId))
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
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", memberEvents.EventId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", memberEvents.MemberId);
            return View(memberEvents);
        }

        // GET: MemberEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberEvents = await _context.MemberEvents
                .Include(m => m.Event)
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.MemberEventsId == id);
            if (memberEvents == null)
            {
                return NotFound();
            }

            return View(memberEvents);
        }

        // POST: MemberEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberEvents = await _context.MemberEvents.FindAsync(id);
            _context.MemberEvents.Remove(memberEvents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberEventsExists(int id)
        {
            return _context.MemberEvents.Any(e => e.MemberEventsId == id);
        }
    }
}
