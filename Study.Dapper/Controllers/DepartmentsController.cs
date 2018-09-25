using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Study.Dapper.DAL;
using Study.Dapper.DL.Entities;

namespace Study.Dapper.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.DepartmentRepository().GetAll());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(long id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var department = await _unitOfWork.DepartmentRepository().Get(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.DepartmentRepository().Insert(department);

                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var department = await _unitOfWork.DepartmentRepository().Get(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.DepartmentRepository().Update(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await DepartmentExists(department.Id))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var department = await _unitOfWork.DepartmentRepository().Get(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var department = await _unitOfWork.DepartmentRepository().Get(id);
            await _unitOfWork.DepartmentRepository().Delete(department);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DepartmentExists(long id)
        {
            return await _unitOfWork.DepartmentRepository().Get(id) != null;
        }
    }
}
