using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeFirst.Models;

namespace CodeFirst.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public JsonResult getEmployeeManagerByDeptId(int departmentId)
        {
            var emp = _context.Managers.Include(x => x.Employees).ThenInclude(x => x.Departments).Where(x => x.DepartmentId == departmentId).ToList();

            return new JsonResult(emp);
        }
        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.projects.Include(p => p.Departments).Include(p => p.Managers);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.projects
                .Include(p => p.Departments)
                .Include(p => p.Managers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Department_Name");
            ViewData["ManagerId"] = new SelectList(_context.Managers, "ManagerId", "ManagerId");
            /* ViewData["ManagerName"] = new SelectList(_context.projects.Include(p => p.Departments).Include(p => p.Managers).ThenInclude(e => e.Employees), "ManagerId", "FirstName");*/
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Project_Name,Description,Domain,StartDate,EndDate,DepartmentId,ManagerId,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Department_Name", project.DepartmentId);
            ViewData["ManagerId"] = new SelectList(_context.Managers, "ManagerId", "ManagerId", project.ManagerId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", project.DepartmentId);
            ViewData["ManagerId"] = new SelectList(_context.Managers, "ManagerId", "ManagerId", project.ManagerId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Project_Name,Description,Domain,StartDate,EndDate,DepartmentId,ManagerId,Status")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Department_Name", project.DepartmentId);
            ViewData["ManagerId"] = new SelectList(_context.Managers, "ManagerId", "ManagerId", project.ManagerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.projects
                .Include(p => p.Departments)
                .Include(p => p.Managers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.projects.FindAsync(id);
            _context.projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.projects.Any(e => e.Id == id);
        }
    }
}
