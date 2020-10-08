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
    public class MembersCreateController : Controller
    {
        private readonly CVGSClubContext _context;

        CVGSClubContext db = new CVGSClubContext();

        public MembersCreateController(CVGSClubContext context)
        {
            _context = context;
        }

        // GET: MembersCreate
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }

        // GET: MembersCreate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (members == null)
            {
                return NotFound();
            }

            return View(members);
        }

        // GET: MembersCreate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembersCreate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,DisplayName,FirstName,LastName,Email,Password,Gender,BirthDate,ReceiveEmails,MailingAddressId,ShippingAddressId,CardType,CardNumber,CardExpires")] Members members)
        {
            if (ModelState.IsValid)
            {
                _context.Add(members);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(members);
        }

        // GET: MembersCreate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members.FindAsync(id);
            if (members == null)
            {
                return NotFound();
            }
            return View(members);
        }

        // POST: MembersCreate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,DisplayName,FirstName,LastName,Email,Password,Gender,BirthDate,ReceiveEmails,MailingAddressId,ShippingAddressId,CardType,CardNumber,CardExpires")] Members members)
        {
            if (id != members.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(members);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembersExists(members.MemberId))
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
            return View(members);
        }

        // GET: MembersCreate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (members == null)
            {
                return NotFound();
            }

            return View(members);
        }

        // POST: MembersCreate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var members = await _context.Members.FindAsync(id);
            _context.Members.Remove(members);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembersExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }

        // Allows the user to login with an existing account
        public IActionResult Login(Members members)
		{
            var _member = db.Members.Where(s => s.DisplayName == members.DisplayName);
            if (_member.Where(s => s.Password == members.Password).Any())
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
            else
            {
                return View();
            }
        }
    }
}
