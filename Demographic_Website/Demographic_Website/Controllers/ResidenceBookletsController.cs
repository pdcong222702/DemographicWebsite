using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demographic_Website.Models;

namespace Demographic_Website.Controllers
{
    public class ResidenceBookletsController : Controller
    {
        private readonly DemoGraphicContext _context;

        public ResidenceBookletsController(DemoGraphicContext context)
        {
            _context = context;
        }

        // GET: ResidenceBooklets
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResidenceBooklets.ToListAsync());
        }

        // GET: ResidenceBooklets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceBooklet = await _context.ResidenceBooklets
                .FirstOrDefaultAsync(m => m.ResidenceBookletId == id);
            if (residenceBooklet == null)
            {
                return NotFound();
            }

            return View(residenceBooklet);
        }

        // GET: ResidenceBooklets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResidenceBooklets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResidenceBookletId,ResidenceBookletCode,Address,MoveDate,MoveReason,BookletArea,CreateDate")] ResidenceBooklet residenceBooklet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(residenceBooklet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(residenceBooklet);
        }

        // GET: ResidenceBooklets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceBooklet = await _context.ResidenceBooklets.FindAsync(id);
            if (residenceBooklet == null)
            {
                return NotFound();
            }
            return View(residenceBooklet);
        }

        // POST: ResidenceBooklets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResidenceBookletId,ResidenceBookletCode,Address,MoveDate,MoveReason,BookletArea,CreateDate")] ResidenceBooklet residenceBooklet)
        {
            if (id != residenceBooklet.ResidenceBookletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(residenceBooklet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenceBookletExists(residenceBooklet.ResidenceBookletId))
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
            return View(residenceBooklet);
        }

        // GET: ResidenceBooklets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceBooklet = await _context.ResidenceBooklets
                .FirstOrDefaultAsync(m => m.ResidenceBookletId == id);
            if (residenceBooklet == null)
            {
                return NotFound();
            }

            return View(residenceBooklet);
        }

        // POST: ResidenceBooklets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residenceBooklet = await _context.ResidenceBooklets.FindAsync(id);
            if (residenceBooklet != null)
            {
                _context.ResidenceBooklets.Remove(residenceBooklet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidenceBookletExists(int id)
        {
            return _context.ResidenceBooklets.Any(e => e.ResidenceBookletId == id);
        }
    }
}
