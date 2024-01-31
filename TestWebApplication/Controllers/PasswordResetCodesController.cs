using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class PasswordResetCodesController : Controller
    {
        private readonly UserContext _context;

        public PasswordResetCodesController(UserContext context)
        {
            _context = context;
        }

        // GET: PasswordResetCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PasswordResetCodes.ToListAsync());
        }

        // GET: PasswordResetCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passwordResetCode = await _context.PasswordResetCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passwordResetCode == null)
            {
                return NotFound();
            }

            return View(passwordResetCode);
        }

        // GET: PasswordResetCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PasswordResetCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Code,Expiration")] PasswordResetCode passwordResetCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passwordResetCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passwordResetCode);
        }

        // GET: PasswordResetCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passwordResetCode = await _context.PasswordResetCodes.FindAsync(id);
            if (passwordResetCode == null)
            {
                return NotFound();
            }
            return View(passwordResetCode);
        }

        // POST: PasswordResetCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Code,Expiration")] PasswordResetCode passwordResetCode)
        {
            if (id != passwordResetCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passwordResetCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasswordResetCodeExists(passwordResetCode.Id))
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
            return View(passwordResetCode);
        }

        // GET: PasswordResetCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passwordResetCode = await _context.PasswordResetCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passwordResetCode == null)
            {
                return NotFound();
            }

            return View(passwordResetCode);
        }

        // POST: PasswordResetCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passwordResetCode = await _context.PasswordResetCodes.FindAsync(id);
            if (passwordResetCode != null)
            {
                _context.PasswordResetCodes.Remove(passwordResetCode);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasswordResetCodeExists(int id)
        {
            return _context.PasswordResetCodes.Any(e => e.Id == id);
        }
    }
}
