using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Controllers
{
    [Authorize(Roles = "admin, teacher")]
    public class AnnouncementController : Controller
    {
        private readonly SchoolDbContext _context;

        public AnnouncementController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Announcement
        public async Task<IActionResult> Index(string search)
        {
            var announcements = from a in _context.Announcements select a;

            if (!string.IsNullOrEmpty(search))
            {
                announcements = announcements.Where(a => a.Title.Contains(search));
            }

            return View(await announcements.ToListAsync());
        }

        // GET: Announcement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: Announcement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Announcement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Date")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }

        // GET: Announcement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        // POST: Announcement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Date")] Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.Id))
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
            return View(announcement);
        }

        // GET: Announcement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: Announcement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }
    }
}
