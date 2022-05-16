using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThedoorCode.Data;
using ThedoorCode.Models;

namespace ThedoorCode.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly UserDbContext _context;

        public ExperienceController(UserDbContext context)
        {
            _context = context;
        }

        // GET: Experience
        public async Task<IActionResult> Index()
        {
            var userDbContext = _context.Experiences.Include(e => e.UserModel);
            return View(await userDbContext.ToListAsync());
        }

        // GET: Experience/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .Include(e => e.UserModel)
                .FirstOrDefaultAsync(m => m.ExperienceId == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // GET: Experience/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.UserModels, "Id", "Name");
            return View();
        }

        // POST: Experience/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExperienceId,UserId,CompanyName,Designation,YearsWorked")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.UserModels, "Id", "Name", experience.UserId);
            return View(experience);
        }

        // GET: Experience/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.UserModels, "Id", "Name", experience.UserId);
            return View(experience);
        }

        // POST: Experience/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExperienceId,UserId,CompanyName,Designation,YearsWorked")] Experience experience)
        {
            if (id != experience.ExperienceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.ExperienceId))
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
            ViewData["UserId"] = new SelectList(_context.UserModels, "Id", "Name", experience.UserId);
            return View(experience);
        }

        // GET: Experience/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .Include(e => e.UserModel)
                .FirstOrDefaultAsync(m => m.ExperienceId == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // POST: Experience/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperienceExists(int id)
        {
            return _context.Experiences.Any(e => e.ExperienceId == id);
        }
    }
}
