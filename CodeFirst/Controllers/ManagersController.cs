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
    public class ManagersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Managers.Include(m => m.Departments).Include(m => m.Employees);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.Departments)
                .Include(m => m.Employees)
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
          public IActionResult Create()
          {
           /* ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Department_Name");
            ViewData["Id"] = new SelectList(_context.Employees, "Id", "FirstName");*/
              return View();
          }
        public JsonResult Department()
        {
            var cnt = _context.Departments.ToList();
            return new JsonResult(cnt);
        }
        public JsonResult Employee(int id)
        {
            var ep = _context.Employees.Where(emp => !(_context.Managers.Select(empMan => empMan.Id).Contains(emp.Id))).Where(x => x.DepartmentId == id).ToList();
           
            /*  var ep = _context.Employees.Where(e => e.Departments.DepartmentId == id ).ToList();*/
            return new JsonResult(ep);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagerId,Id,DepartmentId")] Manager manager,int departmentId)
        {
            if (ModelState.IsValid)
            {

                var existingRank = await _context.Managers.OrderByDescending(x => x.Rank).Where(e => e.DepartmentId == departmentId).Select(x => x.Rank).FirstOrDefaultAsync();
                manager.Rank = existingRank + 1;
                _context.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manager);
        }

        public JsonResult getEmployeeByDeptId(int departmentId)
        {
            var emp = _context.Managers.Include(x => x.Employees).ThenInclude(x => x.Departments).Where(x => x.DepartmentId == departmentId).ToList();
            
            return new JsonResult(emp);
        }
    }

}
    /*    // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Department_Name", manager.DepartmentId);
            ViewData["Id"] = new SelectList(_context.Employees, "Id", "FirstName", manager.Id);
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManagerId,Id,DepartmentId,Rank")] Manager manager)
        {
            if (id != manager.ManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.ManagerId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Department_Name", manager.DepartmentId);
            ViewData["Id"] = new SelectList(_context.Employees, "Id", "FirstName", manager.Id);
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.Departments)
                .Include(m => m.Employees)
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manager = await _context.Managers.FindAsync(id);
            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _context.Managers.Any(e => e.ManagerId == id);
        }

        public ActionResult getDepartment()
        {*//*
           
            return Json(_context.Departments.Select(x => new
            {
                DepartmentID = x.DepartmentId,
                DepartmentName = x.d
            }).ToList(), JsonRequestBehavior.AllowGet);*//*
            return null;
        }
    }
}
*/