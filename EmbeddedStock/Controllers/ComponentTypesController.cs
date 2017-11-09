using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmbeddedStock.Data;
using EmbeddedStock.Models;
using EmbeddedStock.Models.StockViewModels;

namespace EmbeddedStock.Controllers
{
    public class ComponentTypesController : Controller
    {
        private readonly StockContext _context;

        public ComponentTypesController(StockContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            PopulateCategoriesList();

            ViewData["CategoryName"] = "all";

            var viewModel = new ComponentTypeIndexData();

            viewModel.ComponentTypes = await _context.ComponentTypes
                .Include(ct => ct.ComponentTypeCategories)
                 .ThenInclude(ct => ct.Category)
                 .AsNoTracking()
                 .ToListAsync();
            
            return View("Selected",viewModel);
        }

        public async Task<IActionResult> Selected(int? id)
        {
            PopulateCategoriesList();

            if (id == null)
            {
                //show all component types
                return View(await _context.ComponentTypes.ToListAsync());
            }

            ViewData["CategoryID"] = id;

            var viewModel = new ComponentTypeIndexData();
            
            var category = await _context.Categories
                .Include(c => c.ComponentTypeCategories)
                    .ThenInclude(c => c.ComponentType)
                .SingleAsync(c => c.CategoryID == id);

            ViewData["CategoryName"] = category.Name;

            var selectedComponentTypeIds = new List<long>();


            foreach (var ctc in category.ComponentTypeCategories)
            {
                selectedComponentTypeIds.Add(ctc.ComponentType.ComponentTypeID);
            }

            var componentTypes = await _context.ComponentTypes
                .Where(ct => selectedComponentTypeIds.Contains(ct.ComponentTypeID))
                .Include(ct => ct.ComponentTypeCategories)
                    .ThenInclude(ct => ct.Category)
                .ToListAsync();

            viewModel.ComponentTypes = componentTypes;

            return View(viewModel);
        }

        private void PopulateCategoriesList()
        {
            var allCategories = _context.Categories;

            var list = new List<CategoryData>();
            foreach (var category in allCategories)
            {
                list.Add(new CategoryData
                {
                    CategoryID = category.CategoryID,
                    Name = category.Name,
                    Assigned = true

                });
            }

            ViewData["Categories"] = list;
        }

        // GET: ComponentTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = new ComponentTypeIndexData();

            viewModel.ComponentTypes = await _context.ComponentTypes
                .Include(ct => ct.ComponentTypeCategories)
                 .ThenInclude(ct => ct.Category)
                 .AsNoTracking()
                 .ToListAsync();

            ComponentType componentType = viewModel.ComponentTypes.Where(
                    ct => ct.ComponentTypeID == id.Value).Single();
            viewModel.Categories = componentType.ComponentTypeCategories
                .Select(s => s.Category);
                        
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // GET: ComponentTypes/Create
        public IActionResult Create()
        {
           PopulateCategoryData();

            return View();
        }

        // POST: ComponentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentTypeID,ComponentTypeName,ComponentInfo,Location,Status,Datasheet,ImageUrl,Manufacturer,WikiLink,AdminComment")] ComponentType componentType,
            string[] selectedCategories)
        {
            //selected category ids are in selectedCategories array
            AddComponentTypeCategories(selectedCategories, componentType);
            if (ModelState.IsValid)
            {
                
                _context.Add(componentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            PopulateCategoryData(componentType);
            return View(componentType);
            
        }

        private void AddComponentTypeCategories(string[] selectedCategories, ComponentType ctToCreate)
        {
            //set categories of a new component to an empty list 
            ctToCreate.ComponentTypeCategories = new List<ComponentTypeCategory>();
                
            if(selectedCategories == null)
            {
                //if no categories were selected, do nothing else
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
           
            foreach (var category in _context.Categories)
            {
                if (selectedCategoriesHS.Contains(category.CategoryID.ToString()))
                {
                    //if user selected this category, add it to component categories
                    ctToCreate.ComponentTypeCategories.Add(new ComponentTypeCategory
                    {
                        ComponentTypeID = ctToCreate.ComponentTypeID,
                        CategoryID = category.CategoryID
                    });
                    
                }
                
            }
        }

        // GET: ComponentTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .Include(ct => ct.ComponentTypeCategories)
                    .ThenInclude(ct => ct.Category)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ComponentTypeID == id);

            if (componentType == null)
            {
                return NotFound();
            }

            PopulateCategoryData(componentType);
            return View(componentType);
        }
        
        private void PopulateCategoryData(ComponentType componentType = null)
        {
            var allCategories = _context.Categories;
            HashSet<int> ctCategoriesHS;
            if (componentType == null)
            {
                ctCategoriesHS = new HashSet<int>();
            }
            else
            {
                ctCategoriesHS = new HashSet<int>(
                componentType.ComponentTypeCategories
                .Select(ctc => ctc.CategoryID));

            }

            var viewModel = new List<CategoryData>();
            foreach(var category in allCategories)
            {
                viewModel.Add(new CategoryData
                {
                    CategoryID = category.CategoryID,
                    Name = category.Name,
                    Assigned = ctCategoriesHS.Contains(category.CategoryID)
                });
            }

            ViewData["Categories"] = viewModel;
        }

        // POST: ComponentTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ctToUpdate = await _context.ComponentTypes
                .Include(ct => ct.ComponentTypeCategories)
                    .ThenInclude(ct => ct.Category)
                .SingleOrDefaultAsync(m => m.ComponentTypeID == id);

            if (!ModelState.IsValid)
            {
                PopulateCategoryData(ctToUpdate);

                return View(ctToUpdate);
            }

            
            if(await TryUpdateModelAsync<ComponentType>(
                ctToUpdate,
                "",
                ct => ct.ComponentTypeName,
                ct => ct.ComponentInfo,
                ct => ct.Location,
                ct => ct.Status,
                ct => ct.Datasheet,
                ct => ct.ImageUrl,
                ct => ct.Manufacturer,
                ct => ct.AdminComment))
            {
                UpdateComponentTypeCategories(selectedCategories, ctToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "unable to save, try again or ask admin");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateComponentTypeCategories(selectedCategories, ctToUpdate);
            PopulateCategoryData(ctToUpdate);
            return View(ctToUpdate);
            
        }

        private void UpdateComponentTypeCategories(string[] selectedCategories, ComponentType ctToUpdate)
        {
            if(selectedCategories == null)
            {
                ctToUpdate.ComponentTypeCategories = new List<ComponentTypeCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var ctCourses = new HashSet<int>
                (ctToUpdate.ComponentTypeCategories.Select(ct => ct.Category.CategoryID));

            foreach(var category in _context.Categories)
            {
                if (selectedCategoriesHS.Contains(category.CategoryID.ToString()))
                {
                    if(!ctCourses.Contains(category.CategoryID))
                    {
                        ctToUpdate.ComponentTypeCategories.Add(new ComponentTypeCategory
                        {
                            ComponentTypeID = ctToUpdate.ComponentTypeID,
                            CategoryID = category.CategoryID
                        });
                    }
                }
                else
                {
                    if (ctCourses.Contains(category.CategoryID))
                    {
                        ComponentTypeCategory categoryToRemove =
                            ctToUpdate.ComponentTypeCategories
                            .SingleOrDefault(ct => ct.CategoryID == category.CategoryID);
                        _context.Remove(categoryToRemove);
                    }
                }
            }
        }

        // GET: ComponentTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.ComponentTypeID == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // POST: ComponentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var componentType = await _context.ComponentTypes
                .Include(ct => ct.ComponentTypeCategories)
                .SingleAsync(ct => ct.ComponentTypeID == id);

            _context.ComponentTypes.Remove(componentType);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentTypeExists(long id)
        {
            return _context.ComponentTypes.Any(e => e.ComponentTypeID == id);
        }
    }
}
