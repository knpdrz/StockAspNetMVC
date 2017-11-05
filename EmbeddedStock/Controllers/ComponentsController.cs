using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmbeddedStock.Data;
using EmbeddedStock.Models;
using System.Diagnostics;

namespace EmbeddedStock.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly StockContext _context;

        public ComponentsController(StockContext context)
        {
            _context = context;
        }

        // GET: Components
        public async Task<IActionResult> Index()
        {
            var stockContext = _context.Components.Include(c => c.ComponentType);
            return View(await stockContext.ToListAsync());
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .Include(c => c.ComponentType)
                .SingleOrDefaultAsync(m => m.ComponentID == id);
            if (component == null)
            {
                return NotFound();
            }
            
            return View(component);
        }

        // GET: Components/Create
        public IActionResult Create()
        {
            ViewData["ComponentTypeID"] = new SelectList(_context.ComponentTypes, "ComponentTypeID", "ComponentTypeID");
            return View();
        }

        // POST: Components/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ComponentTypeID,ComponentNumber,SerialNo,Status,AdminComment,UserComment,CurrentLoanInformationId")] Component component)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(component);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "unable to save changes, try again or ask admin");
            }
            
           // ViewData["ComponentTypeID"] = new SelectList(_context.ComponentTypes, "ComponentTypeID", "ComponentTypeID", component.ComponentTypeID);
            return View(component);
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components.SingleOrDefaultAsync(m => m.ComponentID == id);
            if (component == null)
            {
                return NotFound();
            }
            ViewData["ComponentTypeID"] = new SelectList(_context.ComponentTypes, "ComponentTypeID", "ComponentTypeID", component.ComponentTypeID);
            return View(component);
        }

        // POST: Components/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }
            var componentToUpdate = await _context.Components
                .SingleOrDefaultAsync(c => c.ComponentID == id);
            if(await TryUpdateModelAsync <Component>(
                componentToUpdate,
                "",
                c => c.ComponentNumber, c=>c.SerialNo, c=>c.Status,
                c=>c.AdminComment,c=>c.UserComment, c=>c.CurrentLoanInformationId,
                c=>c.ComponentTypeID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "unable to save changes, try again or ask admin");
                    Debug.WriteLine("db update err");
                }
            }
            
            //ViewData["ComponentTypeID"] = new SelectList(_context.ComponentTypes, "ComponentTypeID", "ComponentTypeID", component.ComponentTypeID);
            return View(componentToUpdate);
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .Include(c => c.ComponentType)
                .SingleOrDefaultAsync(m => m.ComponentID == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var component = await _context.Components.SingleOrDefaultAsync(m => m.ComponentID == id);
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

        private bool ComponentExists(long id)
        {
            return _context.Components.Any(e => e.ComponentID == id);
        }
    }
}
